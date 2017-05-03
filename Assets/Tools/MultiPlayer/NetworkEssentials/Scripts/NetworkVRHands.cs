using UnityEngine;
using UnityEngine.Networking;

public enum HandSide
{
	Left,
	Right
}

public class NetworkVRHands : NetworkBehaviour
{

    [SyncVar]
	public HandSide side;

    [SyncVar]
	public NetworkInstanceId ownerId;

	private InteractableObject touchedObject;
	private InteractableObject objectInUse;
	private bool hasTriggerBeenPressedThisFrame;
	private bool hasGripBeenPressedThisFrame;
	private bool hasTriggerBeenReleasedThisFrame;
	private bool hasGripBeenReleasedThisFrame;
    private bool isTriggerPressed;
	private bool isGripPressed;

    private InteractableObject grabbedObject;

	private GameObject trackedController;

	private SteamVR_Controller.Device steamDevice;

	private VRPlayerController localPlayer;
    private Vector3 currentVelocity;

    void Start()
    {
		touchedObject = null;
    }

	public override void OnStartAuthority()
	{
		// attach the controller model to the tracked controller object on the local client
		if (hasAuthority) {
			trackedController = GameObject.Find (string.Format ("Controller ({0})", side.ToString("G").ToLowerInvariant()));

            Helper.AttachAtGrip(trackedController.transform, transform);

			localPlayer = ClientScene.FindLocalObject (ownerId).GetComponent<VRPlayerController> ();

			steamDevice = SteamVR_Controller.Input((int)trackedController.GetComponent<SteamVR_TrackedObject>().index);

		}
	}

	void QueryController ()
	{
		hasGripBeenPressedThisFrame = false;
		hasTriggerBeenPressedThisFrame = false;
		hasTriggerBeenReleasedThisFrame = false;
		hasGripBeenReleasedThisFrame = false;
		if (steamDevice.GetTouchDown (SteamVR_Controller.ButtonMask.Trigger)) 
		{
			if (!isTriggerPressed)
				hasTriggerBeenPressedThisFrame = true;
			
			isTriggerPressed = true;
		}
		else if (steamDevice.GetTouchUp (SteamVR_Controller.ButtonMask.Trigger)) 
		{
			if (isTriggerPressed)
				hasTriggerBeenReleasedThisFrame = true;
			isTriggerPressed = false;
		}
		if (steamDevice.GetTouchDown (SteamVR_Controller.ButtonMask.Grip)) 
		{
			if (!isGripPressed)
				hasGripBeenPressedThisFrame = true;
				
			isGripPressed = true;
		}
		else if (steamDevice.GetTouchUp (SteamVR_Controller.ButtonMask.Grip)) 
		{
			if (isGripPressed)
				hasGripBeenReleasedThisFrame = true;
			isGripPressed = false;
		}
        currentVelocity = steamDevice.velocity;
	}

    void Update()
    {
		if (!hasAuthority)
        {
            return;
        }

        QueryController ();

		if (hasGripBeenPressedThisFrame) 
		{
			OnGripPressed ();
		}
		if (hasTriggerBeenPressedThisFrame)
        {
			OnTriggerPressed ();
        }
		else if (hasTriggerBeenReleasedThisFrame)
		{
			OnTriggerReleased ();
		}
    }

	void OnTriggerReleased ()
	{
		Debug.Log ("Trigger released");
		if (grabbedObject != null) {
			// we have an object in this hand
			if (grabbedObject.isUsable) {
				// and it is usable
				CmdStopUsing (grabbedObject.netId, this.netId);
			}
		} else if (touchedObject != null) {
			Debug.Log ("While touching: " + touchedObject.name);
			// we are touch a usable object with this hand
			if (touchedObject.isUsable) {
				// and it is usable
				CmdStopUsing (touchedObject.netId, this.netId);
			}
		} else if (objectInUse != null) {
			Debug.Log ("While using: " + objectInUse.name);
			if (objectInUse.isUsable) {
				// and it is usable
				CmdStopUsing (objectInUse.netId, this.netId);
			}
		}
	}

	void OnTriggerPressed ()
	{
		Debug.Log ("Trigger pressed");
		// interaction requested
		if (grabbedObject != null)
		{
			// we have an object in this hand
			if (grabbedObject.isUsable) 
			{
				// and it is usable
				CmdStartUsing (grabbedObject.netId, this.netId);
				objectInUse = grabbedObject;
			}
		}
		else if (touchedObject != null) {
			Debug.Log ("While touching: " + touchedObject.name);
			// we are touch a usable object with this hand
			if (touchedObject.isUsable) 
			{
				// and it is usable
				CmdStartUsing (touchedObject.netId, this.netId);
				objectInUse = touchedObject;
			}
		}
	}

	void OnGripPressed ()
	{
		Debug.Log ("Grip pressed");
		if (grabbedObject != null) 
		{
			localPlayer.CmdDrop (grabbedObject.netId, currentVelocity);
			grabbedObject = null;
		}
		else if (touchedObject != null)// we have nothing grabbed but something is colliding with our hands
		{
			Debug.Log ("While touching: " + touchedObject.name);
			if (touchedObject.isGrabbable) {
				localPlayer.CmdGrab (touchedObject.netId, netId);	// connectionToClient is only non-null on the player object
				// gets attached to controller in OnStartAuthority of InteractableObject
				grabbedObject = touchedObject;
			}
		}
	}

    void OnTriggerStay(Collider col)
    {
        var iObject = col.gameObject.GetComponent<InteractableObject>();
		if (iObject != null)
        {
			if (touchedObject != iObject) 
			{
				Debug.Log ("Touched Interactable Object: " + iObject.name);
				touchedObject = iObject;
				CmdTouch (touchedObject.netId, netId);
			}
        }
    }

	void OnTriggerExit(Collider col)
    {
        var iObject = col.gameObject.GetComponent<InteractableObject>();
		if (iObject != null && iObject == touchedObject)
		{
			Debug.Log ("UnTouched Interactable Object: " + iObject.name);
			CmdUntouch (touchedObject.netId);
			touchedObject = null;
        }
    }


	[Command]
	public void CmdTouch(NetworkInstanceId objectId, NetworkInstanceId controllerId)
	{
		var iObject = NetworkServer.FindLocalObject (objectId);
		//iObject.GetComponent<InteractableObject> ().touchingControllerId = controllerId;
		var touchable = iObject.GetComponent<ITouchable> ();
		if (touchable != null)
			touchable.Touch(this.netId);
	}

	[Command]
	public void CmdUntouch(NetworkInstanceId objectId)
	{
		var iObject = NetworkServer.FindLocalObject (objectId);
		//iObject.GetComponent<InteractableObject> ().touchingControllerId = NetworkInstanceId.Invalid;
		var touchable = iObject.GetComponent<ITouchable> ();
		if (touchable != null)
			touchable.Untouch(this.netId);
	}

	[Command]
	public void CmdStartUsing(NetworkInstanceId objectNetId, NetworkInstanceId handNetId)
	{
		var item = NetworkServer.FindLocalObject(objectNetId);
		var usable = item.GetComponent<IUsable> ();
		if (usable != null)
			usable.StartUsing(handNetId);
	}

	[Command]
	public void CmdStopUsing(NetworkInstanceId objectNetId, NetworkInstanceId handNetId)
	{
		var item = NetworkServer.FindLocalObject(objectNetId);
		var usable = item.GetComponent<IUsable> ();
		if (usable != null)
			usable.StopUsing(handNetId);
	}


}
