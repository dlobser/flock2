using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_InstantiateEgg : Resetable {

	public GameObject Egg;
	GameObject egg;
	F_AdjustForceOfChildren force;

	// Use this for initialization
	void OnEnable () {
		
		GameObject avatar = GameObject.FindObjectOfType<F_IsLocalPlayer> ().gameObject;
		F_CopyXForms xForms = this.gameObject.AddComponent<F_CopyXForms> ();
		xForms.target = avatar.transform;
		xForms.copyPosition = true;

		egg = Instantiate (Egg);
		egg.transform.position = this.transform.position;
		egg.transform.SetParent (this.transform);

		force = egg.GetComponent<F_AdjustForceOfChildren> ();
	}
	
	// Update is called once per frame
	void OnDisable () {
		GameObject.Destroy (egg);
	}

	public void CrackEgg(){
		egg.transform.parent = null;
		egg.GetComponent<F_AdjustForceOfChildren> ().enabled = true;
	}

	public override void Reset(){
		CrackEgg ();
	}

	void Update(){
		if (!force.doit)
			this.gameObject.SetActive (false);
	}
}
