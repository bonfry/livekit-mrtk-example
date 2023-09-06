using Livekit.Standalone;
using System.Collections.Generic;
using UnityEngine;

public static class RTCEngineManager
{
    private static Dictionary<uint, RTCEngine> engineLookup = new Dictionary<uint, RTCEngine>();
    private static uint engineID = 1;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void Init()
    {
        Debug.LogFormat("Resetting RTCEngineManager");
        engineLookup = new Dictionary<uint, RTCEngine>();
        engineID = 1;
    }

    public static bool ReceiveData(uint handle, out RTCEngine.Message msg)
    {
        var engine = engineLookup[handle];
        if (engine == null)
        {
            Debug.LogErrorFormat("No RTCEngine for handle: {0}", handle);
            msg = new RTCEngine.Message();
            return false;
        }
        return engine.receiveQueue.TryDequeue(out msg);
    }

    public static void SendData(uint handle, byte[] data, uint recipient)
    {
        var engine = engineLookup[handle];
        if (engine == null)
        {
            Debug.LogErrorFormat("No RTCEngine for handle: {0}", handle);
            return;
        }
        engine.SendLossyMessage(data, recipient);
    }

    public static uint GetServerId(uint handle)
    {
        var engine = engineLookup[handle];
        if (engine == null)
        {
            Debug.LogErrorFormat("No RTCEngine for handle: {0}", handle);
            return 0;
        }
        return engine.serverSid;
    }


    public static uint RegisterEngine(RTCEngine engine)
    {
        var id = engineID;
        engineLookup[id] = engine;
        engineID++;
        return id;
    }

    public static void CleanupEngine(uint handle)
    {
        GameObject.Destroy(engineLookup[handle].gameObject);
        engineLookup.Remove(handle);
    }

    public static bool IsConnected(uint handle)
    {
        var engine = engineLookup[handle];
        if (engine == null)
        {
            Debug.LogErrorFormat("No RTCEngine for handle: {0}", handle);
            return false;
        }
        //TODO make this more sophisticaged
        return engine.serverSid != 0;
    }
}
