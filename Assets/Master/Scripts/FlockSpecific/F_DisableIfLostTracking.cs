using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_DisableIfLostTracking : MonoBehaviour {

	public F_CopyXForms xForms;
	// Use this for initialization
	void Start () {
		xForms = this.GetComponent<F_CopyXForms> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (xForms.target == null) {
			this.gameObject.SetActive (false);
		}
	}
}
