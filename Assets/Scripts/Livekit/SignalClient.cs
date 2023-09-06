using Google.Protobuf;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering;

namespace Livekit.Standalone
{
    public class SignalClient : MonoBehaviour
    {
        private string websocketUrl;
        private string token;
        private WebSocket.NativeWebSocket.WebSocket websocket;

        public delegate void OnJoinDelegate(object sender, LiveKit.Proto.JoinResponse response);
        public delegate void OnTrickleDelegate(object sender, LiveKit.Proto.TrickleRequest request);
        public delegate void OnAnswerDelegate(object sender, LiveKit.Proto.SessionDescription answer);
        public delegate void OnOfferDelegate(object sender, LiveKit.Proto.SessionDescription answer);
        public delegate void OnUpdateDelegate(object sender, LiveKit.Proto.ParticipantUpdate update);
        public delegate void OnTrackPublishedDelegate(object sender, LiveKit.Proto.TrackPublishedResponse update);

        // publisher
        public event OnJoinDelegate onJoin;
        public event OnTrickleDelegate onTrickle;
        public event OnAnswerDelegate onAnswer;
        public event OnTrackPublishedDelegate onTrackPublished;

        // subscriber
        public event OnOfferDelegate onOffer;
        public event OnTrickleDelegate onRemoteTrickle;

        // room info
        public event OnUpdateDelegate onUpdate;

        private ConcurrentQueue<byte[]> messageQueue = new ConcurrentQueue<byte[]>();

        public void Join(string websocketUrl, string token)
        {
            this.websocketUrl = websocketUrl;
            this.token = token;
            Connect();
        }

        public async Task SendOffer(LiveKit.Proto.SessionDescription offer)
        {
            var req = new LiveKit.Proto.SignalRequest();
            req.Offer = offer;
            await websocket.Send(req.ToByteArray());
        }

        public async Task SendAnswer(LiveKit.Proto.SessionDescription answer)
        {
            var req = new LiveKit.Proto.SignalRequest();
            req.Answer = answer;
            await websocket.Send(req.ToByteArray());
        }

        public async Task SendIceCandidate(LiveKit.Proto.TrickleRequest candidate)
        {
            var req = new LiveKit.Proto.SignalRequest();
            req.Trickle = candidate;
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            await websocket.Send(req.ToByteArray());
        }

        public async Task SendLeave(LiveKit.Proto.LeaveRequest leaveRequest)
        {
            var req = new LiveKit.Proto.SignalRequest();
            req.Leave = leaveRequest;
            await websocket.Send(req.ToByteArray());
        }

        public async Task SendVideoTrackRequest()
        {
            var req = new LiveKit.Proto.AddTrackRequest
            {
                Source = LiveKit.Proto.TrackSource.Camera,
                Type = LiveKit.Proto.TrackType.Video
            };

            await websocket.Send(req.ToByteArray());
        }

        public async Task SendAudioRequestTrack()
        {
            /*JS {
                cid : "4b710a54-7de5-4a46-af9c-2a42868be10b"
                disableDtx : false
                disableRed : false
                encryption: 0
                height: 0
                layers: []
                muted: false
                name: ""
                sid: ""
                simulcastCodecs: []
                source: 2
                stereo: false
                type: 0
                width: 0    
            }*/

            var req = new LiveKit.Proto.AddTrackRequest
            {
                Cid = "acfb4ed0-c904-4d9e-b0f4-63f759d20b97",
                Source = LiveKit.Proto.TrackSource.Microphone,
                Type = LiveKit.Proto.TrackType.Audio,
                Muted = false,
                DisableDtx = true,
                Stereo = false,
                DisableRed = false,
                
            };


            await websocket.Send(req.ToByteArray());
        }

        public async void Close()
        {
            if (websocket == null)
            {
                return;
            }
            await websocket.Close();
        }

        public IEnumerator UpdateTick()
        {
            while (websocket != null)
            {
                yield return null;
                websocket.DispatchMessageQueue();
            }
        }

        private async void Connect()
        {
            var url = this.websocketUrl + "/rtc?access_token=" + token + "&sdk=js&protocol=9&version=1.9.2&auto_subscribe=1&adaptive_stream=0";
            Debug.LogFormat("[LIVEKIT SERVER WS] Connecting to livekit: {0}", url);
            websocket = new WebSocket.NativeWebSocket.WebSocket(url);

            websocket.OnError += Websocket_OnError;
            websocket.OnOpen += Websocket_OnOpen;
            websocket.OnMessage += Websocket_OnMessage;
            websocket.OnClose += Websocket_OnClose; ;

            StartCoroutine(UpdateTick());
            await websocket.Connect();
        }

        private void Websocket_OnClose(WebSocket.NativeWebSocket.WebSocketCloseCode closeCode)
        {
            Debug.LogFormat("[LIVEKIT SIGNAL CLIENT] On Close: {0}", closeCode);
            websocket = null;
        }

        private void Websocket_OnOpen()
        {
            Debug.LogFormat("[LIVEKIT SIGNAL CLIENT] On Open");
        }

        private void Websocket_OnError(string error)
        {
            Debug.LogFormat("[LIVEKIT SIGNAL CLIENT] On Error: {0}", error);
        }

        private void Websocket_OnMessage(byte[] data)
        {
            try
            {
                var response = LiveKit.Proto.SignalResponse.Parser.ParseFrom(data);
                if (response.Join != null)
                {
                    onJoin.Invoke(this, response.Join);
                }
                else if (response.Trickle != null)
                {
                    if (response.Trickle.Target == LiveKit.Proto.SignalTarget.Publisher)
                    {
                        onTrickle.Invoke(this, response.Trickle);
                    }
                    else
                    {
                        onRemoteTrickle.Invoke(this, response.Trickle);
                    }
                }
                else if (response.Answer != null)
                {
                    onAnswer.Invoke(this, response.Answer);
                }
                else if (response.Offer != null)
                {
                    onOffer.Invoke(this, response.Offer);
                }
                else if (response.Update != null)
                {
                    onUpdate.Invoke(this, response.Update);
                }
                else if (response.TrackPublished != null)
                {
                    Debug.Log("Pubblicata la traccia!!");
                    onTrackPublished.Invoke(this, response.TrackPublished);
                }
            }
            catch (Exception e)
            {
                Debug.LogErrorFormat("[LIVEKIT SIGNAL CLIENT] Error in OnMessage: {0} - {1}", e.Message, data.Length);
            }
            messageQueue.Enqueue(data);
        }
    }
}