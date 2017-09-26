using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_Shadow : MonoBehaviour {

	public GameObject tracker;
	Vector3 pos;
	Vector3 scalar;
	Vector3 rot;
	// Use this for initialization
	void Start () {
		scalar = new Vector3 (1, 0, 1);
		rot = new Vector3 (90, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		pos = tracker.transform.position;
		pos.Scale (scalar);
		this.transform.position = pos;
		this.transform.eulerAngles = rot;
	}
}
