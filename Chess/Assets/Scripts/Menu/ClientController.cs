using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ClientController : NetworkBehaviour {

    public NetworkManager manager;
    private NetworkClient client;

    private bool clienting = false;

	// Use this for initialization
	void Start ()
    {
		if (!manager)
        {
            manager = GameObject.Find("Network Manager").GetComponent<NetworkManager>();
        }

        Application.runInBackground = true;
        DontDestroyOnLoad(transform.gameObject);
	}

    public void RunClient()
    {
        if (!clienting)
        {
            client = new NetworkClient();

            manager.networkAddress = "192.168.11.101";
            manager.networkAddress = "7777";
            client = manager.StartClient();

            clienting = true;
        }
    }

    private void Update()
    {
        if (!clienting)
            RunClient();
    }

}
