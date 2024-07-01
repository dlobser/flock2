using UnityEngine;
using System.Collections;
using Mirror;

public class AutoConnect : MonoBehaviour
{
    private NetworkManagerMirror manager;

    public string autoConnectIP = "127.0.0.1";
    public int autoConnectPort = 7777;

    IEnumerator PingServer()
    {
        while (true)
        {
            Ping ping = new Ping(autoConnectIP);
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
        StartCoroutine(PingServer());

        manager = GetComponent<NetworkManagerMirror>();

        manager.networkAddress = autoConnectIP;
        
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
        while (!NetworkClient.isConnected)
        {
            if (NetworkClient.active)
            {
                manager.StopClient();
                while (NetworkClient.active)
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
                if (NetworkClient.isConnected)
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
            if (!NetworkClient.isConnected)
            {
                Debug.Log("Connection lost, attempting to reconnect...");
                StartCoroutine(Connect());
            }
            yield return new WaitForSeconds(5);
        }
    }
}
