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
	bool doSearch = false;
	GameObject found;
	bool[] filled;
	public bool hideLocal = false;

	public override void OnStartLocalPlayer () {
		trackedObjects = new GameObject[trackedObjectNames.Length];
		networkedObjects = new GameObject[trackedObjectNames.Length];
		filled = new bool[trackedObjectNames.Length];
		StartCoroutine (search ());
	}

	void Update(){
        if (isClient) {
            if (doSearch) {
                finder();
                doSearch = false;
                StartCoroutine(search());
            }
        }

	}

	void finder(){
		found = null;
		for (int i = 0; i < trackedObjectNames.Length; i++) {
			found = GameObject.Find (trackedObjectNames [i]);
			if (found == null && filled[i]) {
				if (filled [i]) {
					filled [i] = false;
					CmdDestroyInstanceNetworkObject(
						i,
						GameObject.FindObjectOfType<F_IsLocalPlayer>().gameObject.GetComponent<NetworkIdentity>()
					);
				}
			} 
			if (found!=null && filled [i] == false) {
				if (NetworkClient.active) {
                    Debug.Log("running here");
					CmdInstanceNetworkObject(
						i,
						GameObject.FindObjectOfType<F_IsLocalPlayer>().gameObject.GetComponent<NetworkIdentity>()
					);
					filled [i] = true;
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
		NetworkServer.SpawnWithClientAuthority( g,id.connectionToClient);
		g.name = networkObject.name + "_" + id.playerControllerId + "_" + which;
		NetworkInstanceId ass =  g.GetComponent<NetworkIdentity> ().netId;
		TargetConnectXform(id.connectionToClient, which,g.name,ass);
    }

	[Command]
	public void CmdDestroyInstanceNetworkObject(int which, NetworkIdentity id){
		GameObject g = GameObject.Find (networkObject.name + "_" + id.playerControllerId + "_" + which);
		NetworkServer.Destroy (g);
	}

	[TargetRpc]
	void TargetConnectXform(NetworkConnection target,int which, string name, NetworkInstanceId ass){
		GameObject g = ClientScene.FindLocalObject (ass);
		g.name = name;
        Debug.Log("LOOOCCCAAALLLLL");
		if (hideLocal) {
			Renderer[] renderers = g.GetComponentsInChildren<Renderer> ();
			for (int i = 0; i < renderers.Length; i++) {
				renderers [i].enabled = false;
			}
		}
		g.GetComponent<F_CopyXForms>().target = GameObject.Find (trackedObjectNames[which]).transform;
	}
}
