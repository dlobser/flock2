using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{
	public class ON_Trigger_ResetBlockCount : ON_TriggerEvents {

	    public float speed;
	    float counter = 0;

		void Start(){
			
		}

	    public override void Ping()
	    {
			base.Ping ();
			interactable.blockCounter = 0;
	    }

		public override void Ping(float t)
		{
		}

		public override void Reset(){
			base.Reset ();
			counter = 0;
		}
	

	}
}
