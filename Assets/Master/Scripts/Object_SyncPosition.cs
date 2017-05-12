using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Object_SyncPosition : NetworkBehaviour
{

	private Transform myTransform;
	[SerializeField] float lerpRate = 5;
	[SyncVar] private Vector3 syncPos;
	[SyncVar] private Quaternion syncRot;
	//    private NetworkIdentity theNetID;

	private Vector3 lastPos;
	private Quaternion lastRot;

	private float threshold = 0.5f;
	float rotThreshold = 1;


	void Start()
	{
		myTransform = GetComponent<Transform>();
		syncPos = GetComponent<Transform>().position;
		syncRot = GetComponent<Transform> ().rotation;
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
			myTransform.rotation = Quaternion.Lerp(myTransform.rotation, syncRot, Time.deltaTime * lerpRate);
		}
	}

	[Command]
	void Cmd_ProvidePositionToServer(Vector3 pos, Quaternion rot)
	{
		syncPos = pos;
		syncRot = rot;
	}

	[ClientCallback]
	void TransmitPosition()
	{
		if (hasAuthority && Quaternion.Angle(myTransform.rotation,lastRot) > rotThreshold || Vector3.Distance(myTransform.position, lastPos) > threshold)
		{
			Cmd_ProvidePositionToServer(myTransform.position, myTransform.rotation);
			lastPos = myTransform.position;
			lastRot = myTransform.rotation;
		}
	}
}