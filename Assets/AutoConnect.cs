using UnityEngine;
using System.Collections;
using Mirror;
using UnityEngine.Networking;

public class AutoConnect : MonoBehaviour
{
    private NetworkManagerMirror manager;

    public string defaultIP = "127.0.0.1";
    public int autoConnectPort = 7777;
    public string remoteIPLocation = "https://pastebin.com/raw/GDwhNP9a";
    public GameObject hideNotAndroid;
    public bool forceAutoConnect;

    private string serverIP;

    IEnumerator PingServer()
    {
        while (true)
        {
            Ping ping = new Ping(serverIP);
            while (!ping.isDone)
            {
                yield return null;
            }
            Debug.Log("Ping to server: " + ping.time + " ms");

            // Wait for 1 second before pinging again
            yield return new WaitForSeconds(1);
        }
    }

    void Start()
    {
        if (Application.platform == RuntimePlatform.Android || forceAutoConnect)
        {
            hideNotAndroid.SetActive(false);
            manager = GetComponent<NetworkManagerMirror>();

            StartCoroutine(GetServerIPAndConnect());
        }
        // else{
        //     this.gameObject.SetActive(false);
        // }
    }

    IEnumerator GetServerIPAndConnect()
    {
        if(!string.IsNullOrEmpty(remoteIPLocation)){
            UnityWebRequest www = UnityWebRequest.Get(remoteIPLocation);
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                serverIP = www.downloadHandler.text.Trim();
            }
            else
            {
                Debug.LogError("Error getting server IP, using default IP. Error: " + www.error);
                serverIP = defaultIP;
            }
        }

        Debug.Log("Using server IP: " + serverIP);

        StartCoroutine(PingServer());

        manager.networkAddress = serverIP;

        // Set the port for the transport component (TelepathyTransport)
        var transport = Transport.activeTransport as TelepathyTransport;
        if (transport != null)
        {
            transport.port = (ushort)autoConnectPort;
        }
        else
        {
            Debug.LogError("Unsupported transport type. Please set the port manually.");
        }

        StartCoroutine(Connect());
        StartCoroutine(CheckConnection());
    }

    IEnumerator Connect()
    {
        while (!Mirror.NetworkClient.isConnected)
        {
            if (Mirror.NetworkClient.active)
            {
                manager.StopClient();
                while (Mirror.NetworkClient.active)
                {
                    yield return null;
                }
                yield return new WaitForSeconds(1);
            }

            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.Log("No network available");
            }
            else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
            {
                Debug.Log("Reachable via carrier data network");
            }
            else if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
            {
                Debug.Log("Reachable via local area network");
            }

            Debug.Log("Attempting to connect to server...");
            manager.StartClient();

            float timeout = Time.time + 5f;  // Timeout for each connection attempt
            while (Time.time < timeout)
            {
                if (Mirror.NetworkClient.isConnected)
                {
                    Debug.Log("Connected to server.");
                    yield break;
                }
                yield return null;
            }

            Debug.Log("Failed to connect, retrying...");
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator CheckConnection()
    {
        while (true)
        {
            if (!Mirror.NetworkClient.isConnected)
            {
                Debug.Log("Connection lost, attempting to reconnect...");
                StartCoroutine(Connect());
            }
            yield return new WaitForSeconds(5);
        }
    }
}

// using UnityEngine;
// using System.Collections;
// using Mirror;

// public class AutoConnect : MonoBehaviour
// {
//     private NetworkManagerMirror manager;

//     public string autoConnectIP = "127.0.0.1";
//     public int autoConnectPort = 7777;

//     public GameObject hideNotAndroid;

//     IEnumerator PingServer()
//     {
//         while (true)
//         {
//             Ping ping = new Ping(autoConnectIP);
//             while (!ping.isDone)
//             {
//                 yield return null;
//             }
//             Debug.Log("Ping to server: " + ping.time + " ms");
            
//             // Wait for 1 second before pinging again
//             yield return new WaitForSeconds(1);
//         }
//     }

//     void Start()
//     {
//         if (Application.platform == RuntimePlatform.Android)
//         {
//             hideNotAndroid.SetActive(false);
//             StartCoroutine(PingServer());

//             manager = GetComponent<NetworkManagerMirror>();

//             manager.networkAddress = autoConnectIP;
            
//             // Set the port for the transport component (TelepathyTransport)
//             var transport = Transport.activeTransport as TelepathyTransport;
//             if (transport != null)
//             {
//                 transport.port = (ushort)autoConnectPort;
//             }
//             else
//             {
//                 Debug.LogError("Unsupported transport type. Please set the port manually.");
//             }

//             StartCoroutine(Connect());
//             StartCoroutine(CheckConnection());
//         }
//         // else{
//         //     this.gameObject.SetActive(false);
//         // }
        
//     }

//     IEnumerator Connect()
//     {
//         while (!NetworkClient.isConnected)
//         {
//             if (NetworkClient.active)
//             {
//                 manager.StopClient();
//                 while (NetworkClient.active)
//                 {
//                     yield return null;
//                 }
//                 yield return new WaitForSeconds(1);
//             }

//             if (Application.internetReachability == NetworkReachability.NotReachable)
//             {
//                 Debug.Log("No network available");
//             }
//             else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
//             {
//                 Debug.Log("Reachable via carrier data network");
//             }
//             else if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
//             {
//                 Debug.Log("Reachable via local area network");
//             }

//             Debug.Log("Attempting to connect to server...");
//             manager.StartClient();

//             float timeout = Time.time + 5f;  // Timeout for each connection attempt
//             while (Time.time < timeout)
//             {
//                 if (NetworkClient.isConnected)
//                 {
//                     Debug.Log("Connected to server.");
//                     yield break;
//                 }
//                 yield return null;
//             }

//             Debug.Log("Failed to connect, retrying...");
//             yield return new WaitForSeconds(1);
//         }
//     }

//     IEnumerator CheckConnection()
//     {
//         while (true)
//         {
//             if (!NetworkClient.isConnected)
//             {
//                 Debug.Log("Connection lost, attempting to reconnect...");
//                 StartCoroutine(Connect());
//             }
//             yield return new WaitForSeconds(5);
//         }
//     }
// }
