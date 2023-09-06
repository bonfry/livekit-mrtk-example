using Livekit.Standalone;
using System.Collections;
using UnityEngine;

namespace Livekit
{
    public class LivekitCallManager : MonoBehaviour
    {
        [SerializeField] private string url;

        [SerializeField] private GameObject selectUserUI;

        [SerializeField] private string userToken1;
        [SerializeField] private string userToken2;

        [SerializeField] RTCEngine rtcEngine;

        private void InitWithToken(string token)
        {
            rtcEngine.Connect(new RTCEngine.ConnectParams
            {
                url = url,
                token = token
            });
        }

        public void InitWithUser1()
        {
            selectUserUI.SetActive(false);
            InitWithToken(userToken1);
        }

        public void InitWithUser2()
        {
            selectUserUI.SetActive(false);
            InitWithToken(userToken2);
        }
    }
}
