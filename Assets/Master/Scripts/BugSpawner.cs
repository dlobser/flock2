using UnityEngine;
using System.Collections;

public class BugSpawner : MonoBehaviour {
	public GameObject actorSynchronizable;

//	private ActorDataSync actorDataSync;
	private Vector3 moveTo;

	// Use this for initialization
	void Start () {
//		actorDataSync = actorSynchronizable.GetComponent<ActorDataSync> ();
	}
	
	// Update is called once per frame
	void Update () {
//		moveTo = actorDataSync.GetGoodSpawnPoint ();
//		moveTo=Vector3.zero;

		this.transform.position = moveTo;
	}
}
