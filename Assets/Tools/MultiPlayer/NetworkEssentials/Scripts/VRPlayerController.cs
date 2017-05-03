using UnityEngine;
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

		if (isLocalPlayer) {
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
