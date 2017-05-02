using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{
	public class ON_Trigger_Emit : ON_TriggerEvents {

	    public ParticleSystem parti;
		public int amount;

	    public override void Ping()
	    {
	        if(parti!=null)
	            parti.Emit(amount);
	    }
	    
	}
}
