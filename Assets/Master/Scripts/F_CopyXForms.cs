using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_CopyXForms : MonoBehaviour {

  public Transform target;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    this.transform.position = target.position;
	}
}
