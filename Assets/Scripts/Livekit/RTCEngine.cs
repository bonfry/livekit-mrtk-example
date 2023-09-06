
using Google.Protobuf;
using Microsoft.MixedReality.WebRTC;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Livekit.Standalone
{
    /*
     non usare Unity.Peerconnection altrimenti non parte la connessione
     */
    public class RTCEngine : MonoBehaviour
    {
        private delegate void OnParticipantUpdate(Participant[] participants);

        public struct ConnectParams
        {
            public string url;
            public string token;
        }

        public struct Message
        {
            public byte[] data;
            public uint sender;
        }

        private PeerConnectionConfiguration config;

        const string lossyDataChannel = "_lossy";
        const string reliableDataChannel = "_reliable";

        private PeerConnection peerConnection;
        private DataChannel lossyDC;
        private DataChannel reliableDC;

        private SignalClient signalClient;

        public ConcurrentQueue<Message> receiveQueue = new ConcurrentQueue<Message>();

        [Space(20)]
        public uint serverSid = 0;
        public string participantSid = null;
        private bool answerReceived = false;
        private bool offerReceived = false;

        private Dictionary<uint, string> sidLookup = new Dictionary<uint, string>();

        [Space(20)]
        [SerializeField] private Microsoft.MixedReality.WebRTC.Unity.AudioTrackSource microphoneSource;
        [SerializeField] private Microsoft.MixedReality.WebRTC.Unity.VideoTrackSource webcamSource;

        [System.Serializable]
        private struct CandidateInit
        {
            public string candidate;
            public int sdpMLineIndex;
            public string sdpMid;
        }


        private void Update()
        {
            if (signalClient != null)
            {
                signalClient.UpdateTick();
            }

            if (receiveQueue.TryDequeue(out Message res))
            {
                Debug.Log($"Received {res.data.Length}: {Encoding.UTF8.GetString(res.data)}");
            }
        }

        private void OnDestroy()
        {
            if (peerConnection != null)
            {
                peerConnection.Close();
                peerConnection.Dispose();
            }
            if (signalClient != null)
            {
                signalClient.Close();
            }
        }

        public void Connect(ConnectParams connectParams)
        {
            config = new PeerConnectionConfiguration
            {
                IceServers = new List<IceServer> {
                    new IceServer { Urls = new List<string> { "stun:stun.l.google.com:19302" } }
                }
            };

            signalClient = gameObject.AddComponent<SignalClient>();
            signalClient.onJoin += SignalClient_OnJoin;
            signalClient.onTrickle += SignalClient_OnTrickle;
            signalClient.onAnswer += SignalClient_OnAnswer;
            signalClient.onOffer += SignalClient_OnOffer;
            signalClient.onUpdate += SignalClient_OnUpdate;
            signalClient.onRemoteTrickle += SignalClient_OnRemoteTrickle;
            signalClient.onTrackPublished += SignalClient_OnTrackPublished;
            signalClient.Join(connectParams.url, connectParams.token);
        }

        #region LiveKitSignalClient Handler

        private void SignalClient_OnJoin(object sender, LiveKit.Proto.JoinResponse joinResponse)
        {
            Debug.LogFormat("[LICEKIT ENGINE] on join");
            participantSid = joinResponse.Participant.Sid;
            var participants = joinResponse.OtherParticipants.Select(pi => new Participant { identity = pi.Identity, sid = pi.Sid }).ToArray();
            foreach (var participant in participants)
            {
                if (participant.identity == "server")
                {
                    serverSid = Util.MurmurHash2.Hash(participant.sid);
                }
                sidLookup[Util.MurmurHash2.Hash(participant.sid)] = participant.sid;
            }
        }

        private void SignalClient_OnTrickle(object sender, LiveKit.Proto.TrickleRequest trickle)
        {
            var parsed = JsonUtility.FromJson<CandidateInit>(trickle.CandidateInit);
            var candidate = new IceCandidate();
            candidate.Content = parsed.candidate;
            candidate.SdpMid = parsed.sdpMid;
            candidate.SdpMlineIndex = parsed.sdpMLineIndex;
            Debug.LogFormat("[LIVEKIT ENGINE PUBLISHER] adding ice candidate: {0}", candidate.Content);
            peerConnection.AddIceCandidate(candidate);
        }

        private void SignalClient_OnRemoteTrickle(object sender, LiveKit.Proto.TrickleRequest trickle)
        {
            var parsed = JsonUtility.FromJson<CandidateInit>(trickle.CandidateInit);

            var candidate = new IceCandidate();
            candidate.Content = parsed.candidate;
            candidate.SdpMid = parsed.sdpMid;
            candidate.SdpMlineIndex = parsed.sdpMLineIndex;

            peerConnection.AddIceCandidate(candidate);
        }

        private async void SignalClient_OnAnswer(object sender, LiveKit.Proto.SessionDescription answer)
        {
            if (answerReceived)
            {
                Debug.Log("RECEIVED ANOTHER ANSWER - SKIPPING");
                return;
            }

            Debug.Log("RECEIVED ANSWER");

            if (answer == null)
            {
                Debug.LogError("[LIVEKIT ENGINE] no answer");
                return;
            }
            if (peerConnection == null)
            {
                Debug.LogError("TODO: No local peer connection");
                return;
            }

            var sdpMsg = new SdpMessage();
            sdpMsg.Content = answer.Sdp;
            sdpMsg.Type = SdpMessageType.Answer;

            Debug.LogFormat("[LIVEKIT ENGINE PUBLISHER] setting remote description: {0}", sdpMsg.Content);
            await peerConnection.SetRemoteDescriptionAsync(sdpMsg);
            answerReceived = true;
        }

        private void SignalClient_OnUpdate(object sender, LiveKit.Proto.ParticipantUpdate update)
        {
            var participants = update.Participants.Select(pi => new Participant { identity = pi.Identity, sid = pi.Sid }).ToArray();
            foreach (var participant in participants)
            {
                if (participant.identity == "server")
                {
                    serverSid = Util.MurmurHash2.Hash(participant.sid);
                }
                sidLookup[Util.MurmurHash2.Hash(participant.sid)] = participant.sid;
            }


        }

        private void SignalClient_OnTrackPublished(object sender, LiveKit.Proto.TrackPublishedResponse response)
        {

            if (response.Track.Type == LiveKit.Proto.TrackType.Audio)
            {
                Debug.Log("Audio track on publish");
            }
            else if (response.Track.Type == LiveKit.Proto.TrackType.Video)
            {
                Debug.Log("Video track on publish");
            }
        }

        private async void SignalClient_OnOffer(object sender, LiveKit.Proto.SessionDescription offer)
        {
            if (peerConnection == null)
            {
                peerConnection = new PeerConnection();
                await peerConnection.InitializeAsync(config);

                peerConnection.RenegotiationNeeded += () =>
                {
                    Debug.LogFormat("[LIVEKIT ENGINE SUBSCRIBER] - on negotiation needed");
                };

                peerConnection.IceCandidateReadytoSend += async e =>
                {
                    Debug.LogFormat("[LIVEKIT ENGINE SUBSCRIBER] on ice candidate");
                    LiveKit.Proto.TrickleRequest trickleReq = new LiveKit.Proto.TrickleRequest();
                    CandidateInit c;
                    c.candidate = e.Content;
                    c.sdpMid = null;
                    c.sdpMLineIndex = 0;
                    trickleReq.CandidateInit = JsonUtility.ToJson(c);
                    trickleReq.Target = LiveKit.Proto.SignalTarget.Publisher;
                    await signalClient.SendIceCandidate(trickleReq);
                };

                peerConnection.IceStateChanged += e =>
                {

                    Debug.LogFormat("[LIVEKIT ENGINE SUBSCRIBER] on ice state change: {0}", e);
                  
                    if (e == IceConnectionState.Connected)
                    {
                       Task.Run(() => signalClient.SendAudioRequestTrack());
                    }
                };

                peerConnection.IceGatheringStateChanged += e =>
                {
                    Debug.LogFormat("[LIVEKIT ENGINE SUBSCRIBER] on ice gathering state change: {0}", e);
                };

                peerConnection.LocalSdpReadytoSend += async sdpMsg =>
                {
                    LiveKit.Proto.SessionDescription protoSessionDescription = new LiveKit.Proto.SessionDescription();
                    protoSessionDescription.Sdp = sdpMsg.Content;
                    protoSessionDescription.Type = "answer";
                    await signalClient.SendAnswer(protoSessionDescription);

                    Debug.LogFormat("[LIVEKIT ENGINE SUBSCRIBER] sent offer");
                };

                peerConnection.DataChannelAdded += e =>
                {
                    Debug.LogFormat("[LIVEKIT ENGINE SUBSCRIBER] ondatachannel sub: {0}", e.Label);
                    if (e.Label == reliableDataChannel)
                    {
                        reliableDC = e;
                        reliableDC.MessageReceived += OnReliableMessage;
                        reliableDC.MessageReceived += msg =>
                        {
                            var msgParsed = Encoding.UTF8.GetString(msg);
                            Debug.Log($"[LIVEKIT ENGINE PUBLISHER] Lossy Message Received ({msg.Length})=> {msgParsed}");
                        };
                    }
                    else if (e.Label == lossyDataChannel)
                    {
                        lossyDC = e;
                        lossyDC.MessageReceived += OnLossyMessage;
                        lossyDC.MessageReceived += msg =>
                        {
                            var msgParsed = Encoding.UTF8.GetString(msg);
                            Debug.Log($"[LIVEKIT ENGINE PUBLISHER] Lossy Message Received ({msg.Length})=> {msgParsed}");
                        };
                    }


                };

            }

            //offerReceived = true;
            await AsyncOnOffer(offer);
            //await signalClient.SendAudioRequestTrack();
        }


        private async Task AsyncOnOffer(LiveKit.Proto.SessionDescription offer)
        {
            Debug.Log("[LIVEKIT ENGINE SUBSCRIBER] got here");
            if (peerConnection == null)
            {
                Debug.LogError("[LIVEKIT ENGINE SUBSCRIBER]: No remote peer connection");
            }

            var sdpMsg = new SdpMessage();
            sdpMsg.Content = offer.Sdp;
            // TODO: use the actual type?
            sdpMsg.Type = SdpMessageType.Offer;
            Debug.LogFormat("[LIVEKIT ENGINE SUBSCRIBER] setting remote description: {0}", sdpMsg.Content);
            await peerConnection.SetRemoteDescriptionAsync(sdpMsg);

            //create and send the answer
            var success = peerConnection.CreateAnswer();

            if (!success)
            {
                Debug.LogError("subscriber - error generating answer");
            }
        }
        #endregion

        #region Data Channel

        private void OnLossyMessage(byte[] bytes)
        {
            Debug.LogError("DC - error generating answer");

            DispatchData(bytes);
        }

        private void OnReliableMessage(byte[] bytes)
        {
            DispatchData(bytes);
        }

        private void DispatchData(byte[] bytes)
        {
            var dp = LiveKit.Proto.DataPacket.Parser.ParseFrom(bytes);
            if (dp.ValueCase == LiveKit.Proto.DataPacket.ValueOneofCase.User)
            {
                var sender = dp.User.ParticipantSid;
                var payload = dp.User.Payload.ToByteArray();

                receiveQueue.Enqueue(new Message { data = payload, sender = Util.MurmurHash2.Hash(sender) });
            }
        }

        public void SendLossyMessage(byte[] bytes, uint recipient)
        {
            if (!sidLookup.TryGetValue(recipient, out var sid))
            {
                Debug.LogWarningFormat("No sid for hash: {0}", recipient);
                return;
            }

            _SendMessage(bytes, new string[] { sid }, false);
        }

        public void SendReliableMessage(byte[] bytes, string[] recipientSids)
        {
            _SendMessage(bytes, recipientSids, true);
        }

        private void _SendMessage(byte[] bytes, string[] recipientSids, bool reliable)
        {
            if (!peerConnection.IsConnected)
            {
                Debug.LogError("TODO - trying to send when peer connection is not connected");
                return;
            }
            var dp = new LiveKit.Proto.DataPacket();
            dp.User = new LiveKit.Proto.UserPacket();
            dp.User.ParticipantSid = this.participantSid;
            dp.User.Payload = ByteString.CopyFrom(bytes);

            if (reliable)
            {
                dp.Kind = LiveKit.Proto.DataPacket.Types.Kind.Reliable;
            }
            else
            {
                dp.Kind = LiveKit.Proto.DataPacket.Types.Kind.Lossy;
            }

            if (recipientSids != null)
            {
                dp.User.DestinationSids.Add(recipientSids);
            }

            if (reliable)
            {
                reliableDC.SendMessage(dp.ToByteArray());
            }
            else
            {
                lossyDC.SendMessage(dp.ToByteArray());
            }
        }

        #endregion
    }
}