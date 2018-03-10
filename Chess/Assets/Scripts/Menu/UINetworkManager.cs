using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UINetworkManager {

    private static bool host = false;
    private static bool client = false;

    public static bool Host
    {
        get { return host; }
        set { host = value; }
    }

    public static bool Client
    {
        get { return client; }
        set { client = value; }
    }

}
