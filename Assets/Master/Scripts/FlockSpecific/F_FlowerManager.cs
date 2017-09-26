using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class F_FlowerManager : NetworkBehaviour {

	public string[] trackedObjectNames;
	GameObject[] trackedObjects;
	GameObject[] networkedObjects;
	public float searchFrequency;
	public GameObject networkObject;
	bool doSearch = true;
	GameObject found;
	bool[] filled;

	void Start () {
		trackedObjects = new GameObject[trackedObjectNames.Length];
		networkedObjects = new GameObject[trackedObjectNames.Length];
		filled = new bool[trackedObjectNames.Length];
		StartCoroutine (search ());
	}

	void Update(){
		if (doSearch) {
			finder ();
			doSearch = false;
			StartCoroutine (search ());
		}

	}

	void finder(){
		found = null;
		for (int i = 0; i < trackedObjectNames.Length; i++) {
			found = GameObject.Find (trackedObjectNames [i]);
			if (found == null && filled[i]) {
				if(filled[i])
					filled[i] = false;
			} 
//			else if (found != null && trackedObjects [i] == null) {
//				trackedObjects [i] = found;
//			}
			//			if (found!=null && trackedObjects [i] != null && networkedObjects [i] == null) {
			//				if (NetworkServer.active) {// || NetworkClient.active) {
			//                    InstanceNetworkObject(i, 
			//                        networkObject,
			//                        found);
			//                    //Debug.Log(g);
			//                    Debug.Log("networrrrkkkk");
			//
			//                    //NetworkServer.SpawnWithClientAuthority( networkedObjects [i],g);
			//
			//                    //NetworkServer.Spawn(networkedObjects[i]);
			//                    //networkedObjects [i].GetComponent<F_CopyXForms> ().target = found.transform;
			//				}
			//			}
			if (found!=null && filled [i] == false) {
				if (NetworkClient.active) {// || NetworkClient.active) {
					Debug.Log("networkclient" + NetworkClient.active);
					CmdInstanceNetworkObject(i ,
//						networkObject,
//						found,
						GameObject.FindObjectOfType<F_IsLocalPlayer>().gameObject.GetComponent<NetworkIdentity>()
//						connectionToClient
					);
					filled [i] = true;
					//Debug.Log(g);
					Debug.Log("networrrrkkkk");
//					Debug.Log(this.GetComponent<NetworkIdentity>().connectionToClient);


					//NetworkServer.SpawnWithClientAuthority( networkedObjects [i],g);

					//NetworkServer.Spawn(networkedObjects[i]);
					//networkedObjects [i].GetComponent<F_CopyXForms> ().target = found.transform;
				}
			}
		}
	}

	IEnumerator search(){
		
		yield return new WaitForSeconds (searchFrequency);
		doSearch = true;
	}


    [Command]
	public void CmdInstanceNetworkObject(int which, NetworkIdentity id){//int which, GameObject toInstance, GameObject found, NetworkConnection conn) {

        GameObject g = (GameObject)Instantiate(networkObject, this.transform.position, this.transform.rotation);
//		GameObject g = (GameObject)Instantiate(networkObject,null);
		NetworkServer.SpawnWithClientAuthority( g,id.connectionToClient);
		g.name = networkObject.name + "_" + id.playerControllerId + "_" + which;
		NetworkInstanceId ass =  g.GetComponent<NetworkIdentity> ().netId;
//        NetworkServer.Spawn(networkedObjects[which]);
//		RpcConnectXForm(which,g.name,ass);
		TargetConnectXform(id.connectionToClient, which,g.name,ass);
//        Debug.Log("spawned " + networkedObjects[which]);
    }
//
//	[ClientRpc]
//	private void RpcConnectXForm(int which, string name, NetworkInstanceId ass)
//	{
//		GameObject g = ClientScene.FindLocalObject (ass);
//		g.name = name;
//
//		g.GetComponent<F_CopyXForms>().target = GameObject.Find (trackedObjectNames[which]).transform;
//	}
	[TargetRpc]
	void TargetConnectXform(NetworkConnection target,int which, string name, NetworkInstanceId ass){
		GameObject g = ClientScene.FindLocalObject (ass);
		g.name = name;
		g.GetComponent<F_CopyXForms>().target = GameObject.Find (trackedObjectNames[which]).transform;
	}
}
