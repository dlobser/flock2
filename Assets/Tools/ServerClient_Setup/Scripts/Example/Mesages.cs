using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using Valve.VR;
using UnityEngine.VR;

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


    //Setting up the SyncVar.
    [SyncVar(hook = "OnVarSynced")]//this needs to be above the variable you want to sync. (you can name the "Hook" everything you want, but be aware that this is the name of a function that is called on the clients when the SyncVar changes on the server)
    public string varToSync; //if this changes on the server a "message" will be send to the clients automaticaly. 
                             // (if you change this on a client it will only change on THAT client, it will not be send anywhere).

    //the "Hook" function is executed on the clients when a change of the SyncVar is recieved.
    //the actual value of the SyncVar on the Clients is not changed when a change mesage is recieved from the server.

    //it is more like a trigger with a "message/variable" attached to it, 
    //it will let clients know "varToSync" has changed on the server and tell them the new value it now has on the server.

	public override void OnStartLocalPlayer ()
	{
		if (!isClient)
			return;
		// delete main camera
		DestroyImmediate (Camera.main.gameObject);

		if (VRDevice.isPresent) {
			// create camera rig and attach player model to it
			vrCameraRigInstance = (GameObject)Instantiate (
				vrCameraRig,
				transform.position,
				transform.rotation);

			Debug.Log (VRDevice.isPresent);

			// ?????
			Transform bodyOfVrPlayer = transform.FindChild ("VRPlayerBody");
			if (bodyOfVrPlayer != null)
				bodyOfVrPlayer.parent = null;

			GameObject head = vrCameraRigInstance.GetComponentInChildren<SteamVR_Camera> ().gameObject;
			transform.parent = head.transform;
			//copyXform.parent = head.transform;
		} else {
			// create camera rig and attach player model to it
			fpsCtrlInstance = (GameObject)Instantiate (
				fpsCtrl,
				transform.position,
				transform.rotation);

			Debug.Log (VRDevice.isPresent);

		

			GameObject head = fpsCtrlInstance.transform.GetChild (0).gameObject;// GetComponentInChildren<SteamVR_Camera> ().gameObject;
			transform.parent = head.transform;
			//copyXform.parent = head.transform;


		}


		if (isLocalPlayer) {
			//copyXform.target = head.transform;
			for (int i = 0; i < AvatarHide.Length; i++) {
				AvatarHide [i].SetActive (false);
			}
			for (int i = 0; i < AvatarShow.Length; i++) {
				AvatarShow [i].SetActive (true);
			}
		}
	}
    //the string "syncedVar" is the new value of "varToSync" that it now has on the server.
    public void OnVarSynced(string syncedVar)//this is the "Hook" function that is called on clients when the Syncvar changes on the server.
    {
        //everyting in here will be executed on the clients.
        inputField.text = syncedVar;

        //you can either use the new variable of "syncedVar" in the "Hook" function immediately or,
        //if you want the "varToSync" to keep the same value as it is on the server (so you can use it later on in the update funtion or somewhere else), 
        //you will have to change it on the clients in the "Hook" function right here.
        varToSync = syncedVar;

        //if you want to do something on the Local player only (the one that you are controlling)
        if (isLocalPlayer)
        {

        }

        //if you want to do something on the Remote players only (the copys of you on the other Clients)
        if (!isLocalPlayer)
        {

        }
    }

    void Update()
    {
        //for this example on the server the Syncvar "varToSync" is the text of an input field.
        //if new text is inputted the Syncvar changes and is sent to the clients.
        if (isServer)
            varToSync = inputField.text;
    }

    //this is used to have a Local Player do someting on the server. (Tell the server to do something)
    [Command]//Send from the Local Client (Called on the Local Client) executed (recieved) on the Server.
    void CmdChangeColor()
    {
        //everything in here will be executed on the Server.
        ChangeColor();

        //as this is send from a Local client, that client basically has "acces" to everything on the server,
        //and can tell/ask the server to do everything that can only be done server side.
    }

    //this is used to have the Server do something on the Clients. (tell clients to do something)
    [ClientRpc]//Send from the Server (Called on the Server) executed (recieved) on the Clients
    void RpcChangeColor()
    {
        //everything in here will be executed on all the Clients (Remote and Local).
        ChangeColor();

        //as this is send from the Server to all clients, the server basically tells the clients to do something on their side.

        //if you want to do something on the Local player only (the one that you are controlling)
        if (isLocalPlayer)
        {
            //I think a "TargetRpc" is being implemented to send Rpc calls to a single Client, but for now this will have to do.
        }

        //if you want to do something on the Remote players only (the copys of you on the other Clients)
        if (!isLocalPlayer)
        {

        }
    }

    /*
        Combining a Rpc call with a command lets you do something on other clients from a local client.
        Send a Rpc call to the server that has a Command in it to tell the clients to do something.

        ---

        [ClientRpc]  //send from local client to server.
        void RpcSendToServer()
        {
            //do something on the server.
            //in this case send a command to the clients
            CmdSendToClients();
        }

        [Command]  //send from server to clients
        void CmdSendToClients()
        {
            //Do something on clients.
        }

        ---

        This could also be done by changing a SyncVar from a local client

        [ClientRpc]  //send from local client to server.
        void RpcSendToServer()
        {
            //Change the SyncVar on the server, and this will trigger the "Hook" function on all clients.           
        }

        ---
    */


    void ChangeColor()
    {
        if (isGreen)
            cubeMat.material.color = Color.yellow;
        else cubeMat.material.color = Color.green;

        isGreen = !isGreen;
    }


    //You can`t put commands and RpcCalls on a button (doesn`t work).
    //So we put these functions on the buttons to trigger the actual Command and Rpc.
    public void ButtonCommand()
    {
        if(!isServer) //if you call a Command from the server you will get an error. (Commands are used to let clients do something on the server)
            CmdChangeColor();
    }

    public void ButtonRPC()
    {
        if(isServer) //if you call a RPC from a client you will get an error. (RPC are used to let the server do something on the clients)
            RpcChangeColor();
    }


}

/*
 * using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;

public class VRPlayerController : NetworkBehaviour
{
	public GameObject vrCameraRig;
//	public GameObject leftHandPrefab;
//    public GameObject rightHandPrefab;
    private GameObject vrCameraRigInstance;
	public GameObject[] masks;
	public GameObject[] show;
    //public F_CopyXForms copyXform;
    //public GameObject birdHead;



 private Transform myTransform;
  [SerializeField] float lerpRate = 5;
  [SyncVar] private Vector3 syncPos;
  //    private NetworkIdentity theNetID;

  private Vector3 lastPos;
  private float threshold = 0.5f;


 void Start()
  {
    myTransform = GetComponent<Transform>();
    syncPos = GetComponent<Transform>().position;
  }


  void FixedUpdate()
  {
    TransmitPosition();
    LerpPosition();
  }


  void LerpPosition()
  {
    if (!hasAuthority)
    {
      myTransform.position = Vector3.Lerp(myTransform.position, syncPos, Time.deltaTime * lerpRate);
    }
  }



  [Command]
  void Cmd_ProvidePositionToServer(Vector3 pos)
  {
    syncPos = pos;
  }

  [ClientCallback]
  void TransmitPosition()
  {
    if (hasAuthority && Vector3.Distance(myTransform.position, lastPos) > threshold)
    {
      Cmd_ProvidePositionToServer(myTransform.position);
      lastPos = myTransform.position;
    }
  }
  


	public override void OnStartLocalPlayer ()
	{
		if (!isClient)
			return;
		// delete main camera
		DestroyImmediate (Camera.main.gameObject);

		// create camera rig and attach player model to it
		vrCameraRigInstance = (GameObject)Instantiate (
			vrCameraRig,
			transform.position,
			transform.rotation);

        // ?????
		Transform bodyOfVrPlayer = transform.FindChild ("VRPlayerBody");
		if (bodyOfVrPlayer != null)
			bodyOfVrPlayer.parent = null;

		GameObject head = vrCameraRigInstance.GetComponentInChildren<SteamVR_Camera> ().gameObject;
        transform.parent = head.transform;
        //copyXform.parent = head.transform;



        if (isLocalPlayer) {
            //copyXform.target = head.transform;
            for (int i = 0; i < masks.Length; i++) {
				masks [i].SetActive (false);
			}
			for (int i = 0; i < show.Length; i++) {
				show [i].SetActive (true);
			}
		}
//        transform.localPosition = new Vector3(0f, -0.03f, -0.06f);

//		TryDetectControllers ();
	}

//	void TryDetectControllers ()
//	{
//		var controllers = vrCameraRigInstance.GetComponentsInChildren<SteamVR_TrackedObject> ();
//        if (controllers != null && controllers.Length == 2 && controllers[0] != null && controllers[1] != null)
//        {
//			CmdSpawnHands(netId);
//        }
//        else
//        {
//            Invoke("TryDetectControllers", 2f);
//        }
//	}
//
//	[Command]
//	void CmdSpawnHands(NetworkInstanceId playerId)
//	{
//        // instantiate controllers
//        // tell the server, to spawn two new networked controller model prefabs on all clients
//        // give the local player authority over the newly created controller models
//        GameObject leftHand = Instantiate(leftHandPrefab);
//		GameObject rightHand = Instantiate(rightHandPrefab);
//
//		var leftVRHand = leftHand.GetComponent<NetworkVRHands> ();
//		var rightVRHand = rightHand.GetComponent<NetworkVRHands> ();
//
//		leftVRHand.side = HandSide.Left;
//		rightVRHand.side = HandSide.Right;
//        leftVRHand.ownerId = playerId;
//		rightVRHand.ownerId = playerId;
//
//		NetworkServer.SpawnWithClientAuthority (leftHand, base.connectionToClient);
//		NetworkServer.SpawnWithClientAuthority (rightHand, base.connectionToClient);
//	}

	[Command]
	public void CmdGrab(NetworkInstanceId objectId, NetworkInstanceId controllerId)
	{
		var iObject = NetworkServer.FindLocalObject (objectId);
		var networkIdentity = iObject.GetComponent<NetworkIdentity> ();
        networkIdentity.AssignClientAuthority(connectionToClient);

        var interactableObject = iObject.GetComponent<InteractableObject>();
        interactableObject.RpcAttachToHand (controllerId);    // client-side
        var hand = NetworkServer.FindLocalObject(controllerId);
        interactableObject.AttachToHand(hand);    // server-side
    }

	[Command]
	public void CmdDrop(NetworkInstanceId objectId, Vector3 currentHolderVelocity)
	{
		var iObject = NetworkServer.FindLocalObject (objectId);
		var networkIdentity = iObject.GetComponent<NetworkIdentity> ();
        networkIdentity.RemoveClientAuthority(connectionToClient);
        
        var interactableObject = iObject.GetComponent<InteractableObject>();
        interactableObject.RpcDetachFromHand(currentHolderVelocity); // client-side
        interactableObject.DetachFromHand(currentHolderVelocity); // server-side
    }
}
*/