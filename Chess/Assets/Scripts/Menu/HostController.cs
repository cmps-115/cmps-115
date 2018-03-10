using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HostController : NetworkBehaviour {

    public NetworkManager manager;

    private bool hosting = false;

	// Use this for initialization
	void Start ()
    {

       if (!manager)
           manager = GameObject.Find("Network Manager").GetComponent<NetworkManager>();
    }

    public void RunHost()
    {
        if (!hosting)
        {
            manager.maxConnections = 1;
            manager.networkPort = 7777;
            manager.StartServer();

            if (NetworkServer.active)
            {
                Debug.Log("Yay");
            }

            hosting = true;
        }
        else
        {
            manager.StopServer();
            hosting = false;
        }
    }
	

}
