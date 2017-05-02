using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{
	public class ON_Trigger_Example : ON_TriggerEvents {

	    public float speed;
	    float counter = 0;

		void Start(){
			
		}

	    public override void Ping()
	    {
			Debug.Log ("Example Ping");
			base.Ping ();
	        StartCoroutine(HandlePing());
	    }

		public override void Ping(float t)
		{
			Animate (t);
		}

		public override void Reset(){
			base.Reset ();
			counter = 0;
		}
	

	    IEnumerator HandlePing()
	    {
			counter = 0;
	        while (counter < speed)
	        {
	            counter += Time.deltaTime;
				Debug.Log ("Example: " + counter);
	            yield return new WaitForSeconds(Time.deltaTime);
	        }
			Reset();
			yield return null;
	    }

		void Animate(float t){
			Debug.Log ("Example: " + t);
		}
	}
}
