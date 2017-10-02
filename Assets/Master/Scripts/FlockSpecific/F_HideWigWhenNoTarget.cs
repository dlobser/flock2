using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_HideWigWhenNoTarget : MonoBehaviour {

	public GameObject Wig;
	MeshRenderer rend;
	F_CopyXForms xForm;
	// Use this for initialization
	void Start () {
		xForm = GetComponent<F_CopyXForms> ();
		rend = Wig.GetComponent<MeshRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (xForm.target == null || !xForm.target.gameObject.activeInHierarchy) {
			rend.material.SetFloat ("_Thickness", 0);
			rend.enabled = false;

			Debug.Log ("hide");
		}
	}
}
