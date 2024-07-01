using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using Valve.VR;
using UnityEngine.XR;

public class Mesages : NetworkBehaviour {

    //for example purposes
    public InputField inputField;
    public Renderer cubeMat;
    public bool isGreen;

    public GameObject vrCameraRig;
    public GameObject fpsCtrl;
    GameObject vrCameraRigInstance;
    GameObject fpsCtrlInstance;

    public GameObject[] AvatarHide;
    public GameObject[] AvatarShow;

    // Setting up the SyncVar
    [SyncVar(hook = "OnVarSynced")]
    public string varToSync;

    public override void OnStartLocalPlayer ()
    {
        if (!isClient)
            return;

        // delete main camera
        DestroyImmediate(Camera.main.gameObject);

        if (XRSettings.isDeviceActive) {
            // create camera rig and attach player model to it
            vrCameraRigInstance = (GameObject)Instantiate(vrCameraRig, transform.position, transform.rotation);

            Debug.Log(XRSettings.isDeviceActive);

            // ?????
            Transform bodyOfVrPlayer = transform.Find("VRPlayerBody");
            if (bodyOfVrPlayer != null)
                bodyOfVrPlayer.parent = null;

            GameObject head = vrCameraRigInstance.GetComponentInChildren<SteamVR_Camera>().gameObject;
            transform.parent = head.transform;
        } else {
            // create camera rig and attach player model to it
            fpsCtrlInstance = (GameObject)Instantiate(fpsCtrl, transform.position, transform.rotation);

            Debug.Log(XRSettings.isDeviceActive);

            GameObject head = fpsCtrlInstance.transform.GetChild(0).gameObject;
            transform.parent = head.transform;
        }

        if (isLocalPlayer) {
            for (int i = 0; i < AvatarHide.Length; i++) {
                AvatarHide[i].SetActive(false);
            }
            for (int i = 0; i < AvatarShow.Length; i++) {
                AvatarShow[i].SetActive(true);
            }
        }
    }

    // the string "syncedVar" is the new value of "varToSync" that it now has on the server.
    public void OnVarSynced(string syncedVar)
    {
        // everything in here will be executed on the clients.
        inputField.text = syncedVar;

        // if you want the "varToSync" to keep the same value as it is on the server,
        // you will have to change it on the clients in the "Hook" function right here.
        varToSync = syncedVar;

        // if you want to do something on the Local player only (the one that you are controlling)
        if (isLocalPlayer)
        {
            // Local player specific logic
        }

        // if you want to do something on the Remote players only (the copies of you on the other Clients)
        if (!isLocalPlayer)
        {
            // Remote player specific logic
        }
    }

    void Update()
    {
        // for this example on the server the SyncVar "varToSync" is the text of an input field.
        // if new text is inputted the SyncVar changes and is sent to the clients.
        if (isServer)
            varToSync = inputField.text;
    }

    // this is used to have a Local Player do something on the server.
    [Command]
    void CmdChangeColor()
    {
        // everything in here will be executed on the Server.
        ChangeColor();
    }

    // this is used to have the Server do something on the Clients.
    [ClientRpc]
    void RpcChangeColor()
    {
        // everything in here will be executed on all the Clients (Remote and Local).
        ChangeColor();

        // if you want to do something on the Local player only (the one that you are controlling)
        if (isLocalPlayer)
        {
            // Local player specific logic
        }

        // if you want to do something on the Remote players only (the copies of you on the other Clients)
        if (!isLocalPlayer)
        {
            // Remote player specific logic
        }
    }

    void ChangeColor()
    {
        if (isGreen)
            cubeMat.material.color = Color.yellow;
        else
            cubeMat.material.color = Color.green;

        isGreen = !isGreen;
    }

    // You can't put commands and RpcCalls on a button (doesn't work).
    // So we put these functions on the buttons to trigger the actual Command and Rpc.
    public void ButtonCommand()
    {
        if(!isServer) // if you call a Command from the server you will get an error.
            CmdChangeColor();
    }

    public void ButtonRPC()
    {
        if(isServer) // if you call a RPC from a client you will get an error.
            RpcChangeColor();
    }
}
