using Livekit;
using UnityEngine;

public class WebRTCManager : MonoBehaviour
{
    [SerializeField] private LivekitCallManager callManager;
    [SerializeField] private GameObject selectUserUI;

    [SerializeField] private string userToken1;
    [SerializeField] private string userToken2;

    public void SelectUser1() {
        selectUserUI.SetActive(false);
        callManager.InitWithToken(userToken1);
    }

    public void SelectUser2() {
        selectUserUI.SetActive(false);
        callManager.InitWithToken(userToken2);
    }
}
