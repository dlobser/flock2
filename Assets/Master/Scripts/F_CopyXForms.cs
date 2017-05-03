using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_CopyXForms : MonoBehaviour {

  	public Transform target;
	public bool copyCamera;
	public bool copyRotation;
	// Use this for initialization
	void Start () {
		if (copyCamera || target==null)
			target = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(copyCamera&&target!=Camera.main.transform)
			target = Camera.main.transform;
		this.transform.position = target.position;
		if (copyRotation)
			this.transform.rotation = target.rotation;
	}
}
