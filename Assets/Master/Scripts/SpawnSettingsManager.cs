using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnSettingsManager : NetworkBehaviour {

    public GameObject settings;
	// Use this for initialization
	void Start () {
        NetworkServer.Spawn(settings);
        Debug.Log("spawned");
	}
	
	//// Update is called once per frame
	//void Update () {
		
	//}
}
