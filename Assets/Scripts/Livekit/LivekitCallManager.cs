using Livekit.Standalone;
using System.Collections;
using UnityEngine;

namespace Livekit
{
    public class LivekitCallManager : MonoBehaviour
    {
        [SerializeField] private string url;
        [SerializeField] private string token;

        [SerializeField] RTCEngine rtcEngine;

        public void InitWithToken(string token)
        {
            rtcEngine.Connect(new RTCEngine.ConnectParams
            {
                url = url,
                token = token
            });
        }
    }
}
