using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_InstantiateEgg : Resetable {

	public GameObject Egg;
	GameObject egg;
	F_AdjustForceOfChildren force;

	// Use this for initialization
	void OnEnable () {

        if (GameObject.FindObjectOfType<F_IsLocalPlayer>() != null) {
            GameObject avatar = GameObject.FindObjectOfType<F_IsLocalPlayer>().gameObject;
            F_CopyXForms xForms = this.gameObject.AddComponent<F_CopyXForms>();
            xForms.target = avatar.transform;
            xForms.copyPosition = true;

            egg = Instantiate(Egg);
            egg.transform.position = this.transform.position;
            egg.transform.SetParent(this.transform);

            force = egg.GetComponent<F_AdjustForceOfChildren>();
        }
        else this.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void OnDisable () {
		GameObject.Destroy (egg);
	}

	public void CrackEgg(){
		egg.transform.parent = null;
		egg.GetComponent<F_AdjustForceOfChildren> ().enabled = true;
		egg.GetComponent<AudioSource> ().Play ();
	}

	public override void Reset(){
		if(egg!=null)
			CrackEgg ();
	}

	void Update(){
		if (!force.doit)
			this.gameObject.SetActive (false);
        //Debug.Log(force.doit);
	}
}
