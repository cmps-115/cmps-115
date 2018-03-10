using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkSelector : MonoBehaviour {

    public NetworkManagerHUD manager;

    private static bool connected = false;
	// Use this for initialization
	private void Start () {

        if (connected)
            return;

        manager = GetComponent<NetworkManagerHUD>();

        if (UINetworkManager.Host)
            manager.manager.StartHost();
        else
            manager.manager.StartClient();

        connected = true;
    }
	
}
