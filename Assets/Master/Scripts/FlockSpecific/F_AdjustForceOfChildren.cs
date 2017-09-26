using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_AdjustForceOfChildren : MonoBehaviour {

	ConstantForce[] cForce;
	public Vector3 Force;
	public bool doit = true;
	// Use this for initialization
	void Start () {
		cForce = GetComponentsInChildren<ConstantForce> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (this.transform.childCount>0 && doit) {
			for (int i = 0; i < cForce.Length; i++) {
				if (cForce [i] != null) {
					if (cForce [i].GetComponent<Rigidbody> ().isKinematic)
						cForce [i].GetComponent<Rigidbody> ().isKinematic = false;
					cForce [i].force = Force;
				}
			}

		}
		else
			doit = false;
	}
}
