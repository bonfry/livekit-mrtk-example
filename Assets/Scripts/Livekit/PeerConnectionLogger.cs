using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeerConnectionLogger : MonoBehaviour
{
    public void OnPeerConnected()
    {
        Debug.Log("[WebRTC Peer] Local Peer connected!");
    }

    public void OnPeerDisconected()
    {
        Debug.Log("[WebRTC Peer] Local Peer disconnected!");
    }

    public void OnPeerError(string err)
    {
        Debug.Log($"[WebRTC Peer] Local Peer error: {err}!");
    }
}
