using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Object_SyncPosition : NetworkBehaviour
{

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
}