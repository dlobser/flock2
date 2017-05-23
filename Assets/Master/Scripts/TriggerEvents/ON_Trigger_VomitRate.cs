using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{
	public class ON_Trigger_VomitRate : ON_TriggerEvents {

		public float rate;
		float counter = 0;
		public F_VomitCtrl vCtrl;

		void Start(){

		}

		public override void Ping()
		{
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
			while (counter < rate)
			{
				counter += Time.deltaTime;
				Animate (counter);
				yield return new WaitForSeconds(Time.deltaTime);
			}
			Reset();
			yield return null;
		}

		void Animate(float t){
			vCtrl.rate = t * rate;
		}
	}
}
