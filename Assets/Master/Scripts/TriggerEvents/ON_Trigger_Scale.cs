using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{
	public class ON_Trigger_Scale : ON_TriggerEvents {

	    public Vector3 scale;
		private Vector3 initialScale;
		private Vector3 privateScale;
		private Vector3 init;
		public bool randomize = false;
		public bool smoothMove = false;
		public bool relativeScale = false;
		public float timeToScale = 1;

		public bool pingpong;


		float counter = 0;
//		bool triggered = false;

		public override void OnEnable(){
			base.OnEnable ();
			GetComponent<ON_InteractableEvents> ().OnIdle += Idle;

			initialScale = scale;
			privateScale = scale;

		}

		public override void OnDisable(){
			base.OnDisable ();
			GetComponent<ON_InteractableEvents> ().OnIdle -= Idle;
		}

	    public override void Ping() {
			base.Ping ();
			counter = 0;
			GetInitialPosition ();
			StartCoroutine (HandlePing ());
	    }

		public override void Ping(float t){
			if (!triggered)
				GetInitialPosition ();
			Animate (t);
			triggered = true;
		}

		public override void Reset(){
			base.Reset ();
			Idle ();
			counter = 0;
			triggered = false;
		}

		void Idle(){
//			privateScale = relativeScale ? this.transform.localScale + initialScale : privateScale;
			GetInitialPosition ();
		}

		void GetInitialPosition(){
			init = this.transform.localScale;
			privateScale = relativeScale ? this.transform.localScale + initialScale : privateScale;

			if (randomize) {
				privateScale = scale;
				privateScale.Scale (Random.insideUnitSphere);

			
			}
		}

		IEnumerator HandlePing(){
			counter = 0;
			while (counter < timeToScale) {
				counter += Time.deltaTime;
				Animate (counter / timeToScale);
				yield return null;
			}
			if(pingpong)
				StartCoroutine(UnAnimate ());
			else
				Reset ();
		}

		IEnumerator UnAnimate(){
			while (counter > 0) {
				counter -= Time.deltaTime;
				Animate (counter / timeToScale);
				yield return null;
			}
			Reset ();
		}

		void Animate(float t){
			if (smoothMove)
				t = Mathf.SmoothStep (0, 1, t);
			this.transform.localScale = Vector3.Lerp (init,  privateScale, t);
			
		}
	}
}
