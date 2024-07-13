using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System;
using System.IO;
using System.Collections;
using System.Net;
using System.Net.NetworkInformation;
using System.Linq;
using UnityEngine.Networking;

public class ServerHUDMirror : MonoBehaviour
{
    public GameObject stopServer, startHost, startServer, resetSettings, getIP, checking, clientsInfo;
    public Text serverInfoText, portPlaceholderText, paswPlaceholderText, clientsInfoText;
    public InputField portText, passwordText, maxConnText;

    private NetworkManagerMirror manager;
    private bool noConnection, setText, checkIP;
    private string externalip = "?", localIP = "?";
    private int maximumConnections;

    void Start()
    {
        manager = GetComponent<NetworkManagerMirror>();

        // Checking if we have saved Server Information and filling the text fields.
        if (PlayerPrefs.HasKey("nwPortS"))
        {
            portPlaceholderText.text = PlayerPrefs.GetString("nwPortS");
        }
        if (PlayerPrefs.HasKey("IPAddressS"))
        {
            externalip = PlayerPrefs.GetString("IPAddressS");
            localIP = PlayerPrefs.GetString("LocalIP");
            getIP.GetComponentInChildren<Text>().text = "Server IP Address\nExternal :" + externalip + "\nLocal :" + localIP;
        }
        if (PlayerPrefs.HasKey("Password"))
        {
            passwordText.text = PlayerPrefs.GetString("Password");
            if (passwordText.text == "")
                paswPlaceholderText.text = "(not needed)";
        }
        if (PlayerPrefs.HasKey("MaxConnections"))
        {
            maxConnText.text = PlayerPrefs.GetString("MaxConnections");
        }

        clientsInfoText = clientsInfo.GetComponentInChildren<Text>();
        setText = true;
    }

    void Update()
    {
        noConnection = (Mirror.NetworkClient.active == false || Mirror.NetworkClient.isConnected == false);
        // Showing and hiding the appropriate buttons and text depending on if the server is running or not.
        if (!Mirror.NetworkServer.active)//!Mirror.NetworkClient.isConnected && !Mirror.NetworkServer.active)
        {
            GetIP();
            if (noConnection)
            {
                stopServer.SetActive(false);
                clientsInfo.SetActive(false);

                if (setText)
                {
                    serverInfoText.color = Color.red;
                    serverInfoText.text = "Server Not Running !";
                    // setText = false;
                }
            }
        }
        else
        {
            if (setText)
            {
                serverInfoText.color = new Color(0.2f, 0.6f, 0.2f, 1f);
                string pw = passwordText.text == "" ? "(no password)" : passwordText.text;
                string maxConn = maxConnText.text == "" ? "8" : maxConnText.text;

                serverInfoText.text = "Server Is Running !\n" + "\nIP Address\nExternal : " + externalip + "\nLocal : " + localIP + "\n\nServer Port : " + portPlaceholderText.text + "\nPassword : " + pw + "\nMax Connections : " + maxConn;
                // setText = false;
            }
        }
    }

    public void StopHostCustom()
    {
        startServer.SetActive(true);
        startHost.SetActive(true);
        portText.transform.parent.gameObject.SetActive(true);
        resetSettings.SetActive(true);
        getIP.SetActive(true);
        setText = true;
        manager.StopHost();
    }

    public void StartServerCustom()
    {
        SetPort();
        PlayerPrefs.SetString("Password", passwordText.text); // Save the server's password.  
        PlayerPrefs.SetString("MaxConnections", maxConnText.text.ToString());

        ShowAndHideButtons();

        manager.maxConnections = GetMaximumConnections();
        manager.StartServer();
    }

    public void StartHostCustom()
    {
        SetPort();
        PlayerPrefs.SetString("Password", passwordText.text); // Save the server's password.  
        PlayerPrefs.SetString("MaxConnections", maxConnText.text.ToString());

        ShowAndHideButtons();

        manager.maxConnections = GetMaximumConnections();
        manager.StartHost();
    }

    public void ResetToDefault()
    {
        // Deleting all saved info and resetting to use the default ones.
        PlayerPrefs.DeleteKey("IPAddressS");
        getIP.GetComponentInChildren<Text>().text = "Find Server IP Address.";
        externalip = "?";
        PlayerPrefs.DeleteKey("nwPortS");
        portPlaceholderText.text = "7777(Default)";
        portText.text = "";
        PlayerPrefs.DeleteKey("LocalIP");
        localIP = "?";
        PlayerPrefs.DeleteKey("Password");
        paswPlaceholderText.text = "(not needed)";
        passwordText.text = "";
        PlayerPrefs.DeleteKey("MaxConnections");
        maxConnText.text = "";
    }

    public void GetIP()
    {
        getIP.GetComponentInChildren<Text>().text = "If this takes too long\nClick again.";
        StartCoroutine(GetPublicIP()); // Start the actual checking.
        checking.SetActive(true);
    }

    IEnumerator GetPublicIP()
    {
        UnityWebRequest request = UnityWebRequest.Get("http://checkip.dyndns.org");
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.ConnectionError && request.result != UnityWebRequest.Result.ProtocolError)
        {
            // Filter the response message for the IP address.
            string response = request.downloadHandler.text;
            string[] a = response.Split(':');
            string a2 = a[1].Substring(1);
            string[] a3 = a2.Split('<');
            string a4 = a3[0];
            externalip = a4; // TADAA..!! Your external IP address :)

            // Getting the IP from the PC the server is running on (a local LAN address)
            localIP = GetLocalIPAddress();

            getIP.GetComponentInChildren<Text>().text = "Server IP Address\nExternal :" + externalip + "\nLocal :" + localIP;
            // Saving the IP addresses.
            PlayerPrefs.SetString("IPAddressS", externalip);
            PlayerPrefs.SetString("LocalIP", localIP);
            checking.SetActive(false);
        }
        else
        {
            getIP.GetComponentInChildren<Text>().text = "Something went wrong\nPlease try again";
            checking.SetActive(false);
        }
    }

    private void SetPort()
    {
        if (string.IsNullOrEmpty(portText.text))
        {
            if (PlayerPrefs.HasKey("nwPortS"))
            {
                portPlaceholderText.text = PlayerPrefs.GetString("nwPortS");
            }
            else
            {
                portPlaceholderText.text = "7777(Default)";
            }
        }
        else
        {
            PlayerPrefs.SetString("nwPortS", portText.text); // Save the port we are using.
            portPlaceholderText.text = portText.text;
        }

         var transport = Transport.activeTransport as TelepathyTransport;
        if (transport != null)
        {
            transport.port = ushort.Parse(portPlaceholderText.text);
        }
        else
        {
            Debug.LogError("Unsupported transport type. Please set the port manually.");
        }

    }

    private void ShowAndHideButtons()
    {
        resetSettings.SetActive(false);
        portText.transform.parent.gameObject.SetActive(false);
        getIP.SetActive(false);
        startServer.SetActive(false);
        startHost.SetActive(false);
        stopServer.SetActive(true);
        clientsInfo.SetActive(true);
        setText = true;
    }

    private int GetMaximumConnections()
    {
        if (!string.IsNullOrEmpty(maxConnText.text))
        {
            return Convert.ToInt32(maxConnText.text);
        }
        else
        {
            return 8;
        }
    }

    private string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                // Debug.Log("This IP: " + ip.ToString());
                return ip.ToString();
            }
        }
        throw new Exception("No network adapters with an IPv4 address in the system!");
    }
}
