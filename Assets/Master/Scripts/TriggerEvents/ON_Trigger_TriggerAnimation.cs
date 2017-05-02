using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{
	public class ON_Trigger_TriggerAnimation : ON_TriggerEvents {

		public string TriggerName;

	    public override void Ping()
	    {
			base.Ping ();
			this.GetComponent<Animator> ().SetTrigger (TriggerName);

	    }

	}
}
