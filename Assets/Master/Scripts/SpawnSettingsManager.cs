using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnSettingsManager : NetworkBehaviour {

    public GameObject settings;
    bool spawned;
	// Use this for initialization
	void Start () {
       
	}

    // Update is called once per frame
    void Update()
    {
        if (!spawned && NetworkServer.active)
        {
            NetworkServer.Spawn(settings);
            Debug.Log("spawned");
            spawned = true;
        }
        Debug.Log(NetworkServer.active);
    }
}
