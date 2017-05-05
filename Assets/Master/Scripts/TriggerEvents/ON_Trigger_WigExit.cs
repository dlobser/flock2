using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{
	public class ON_Trigger_WigExit : ON_TriggerEvents {

	    public float speed;
		public float lengthMax;
	    float counter = 0;
		public GameObject Wig;


		void Start(){
			if (Wig == null)
				Wig = GameObject.Find ("Wig");
		
		}

	    public override void Ping()
	    {
			Debug.Log ("Wigged out");
			base.Ping ();
			if (Wig == null)
				Wig = GameObject.Find ("Wig");
			if (Wig != null) {
				Wig.GetComponent<MeshRenderer> ().enabled = false;
			}

	    }

		public override void Ping(float t)
		{
		}

		public override void Reset(){
			base.Reset ();
			counter = 0;
			if (Wig == null)
				Wig = GameObject.Find ("Wig");
			Wig.GetComponent<MeshRenderer> ().enabled = false;
		}
	

	  
	}
}
