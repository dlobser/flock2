using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class F_Players : object {

//	public List<GameObject> avatars { get; set; }
//	float counter = 0;
//	public float checkFrequency = 1;
//	public bool debug;

	public static GameObject thisPlayer { get; set; }
	public static int thisPlayerID = -1;
	public static int players {get{return GameObject.FindGameObjectsWithTag ("Avatar").Length;}}
	// Use this for initialization
//	void Start () {
//		avatars = new List<GameObject> ();
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		counter += Time.deltaTime;
//
//		if (counter > checkFrequency) {
//			avatars = GameObject.FindGameObjectsWithTag ("Avatar").ToList ();
//			if (debug) {
//				for (int i = 0; i < avatars.Count; i++) {
//					Debug.Log (avatars [i] + " avatar " + i);
//				}
//			}
//			counter = 0;
//		}
//	}
}
