using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{
	public class ON_Trigger_Rotate : ON_TriggerEvents {

	    public Vector3 rotation;
		private Vector3 initialRotation;
		private Vector3 privateRotation;
		private Vector3 init;
		public bool randomize = false;
		public bool smoothMove = false;
		public bool relativeRotation = false;
		public float timeToRotate = 1;
		public GameObject objectToRotate;
		public bool pingpong;


		float counter = 0;
//		bool triggered = false;

		void Awake(){
      init = objectToRotate.transform.localEulerAngles;

      if (objectToRotate == null)
				objectToRotate = this.gameObject;
			
		}

		public override void OnEnable(){
			base.OnEnable ();


			GetComponent<ON_InteractableEvents> ().OnIdle += Idle;

			initialRotation = rotation;
			privateRotation = rotation;

		}

		public override void OnDisable(){
			base.OnDisable ();
			GetComponent<ON_InteractableEvents> ().OnIdle -= Idle;
		}

	    public override void Ping() {
			base.Ping ();
			counter = 0;
			GetInitialRotation ();
			StartCoroutine (HandlePing ());
	    }

		public override void Ping(float t){
			if (!triggered)
				GetInitialRotation ();
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
			GetInitialRotation ();
		}

		void GetInitialRotation(){
			//if(!triggered)
			//	init = objectToRotate.transform.localEulerAngles;
			privateRotation = relativeRotation ? objectToRotate.transform.localEulerAngles + initialRotation : privateRotation;

			if (randomize) {
				privateRotation = rotation;
				privateRotation.Scale (Random.insideUnitSphere);

			
			}
		}

		IEnumerator HandlePing(){
			counter = 0;
			while (counter < timeToRotate) {
				counter += Time.deltaTime;
				Animate (counter / timeToRotate);
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
				Animate (counter / timeToRotate);
				yield return null;
			}
			Reset ();
		}

		void Animate(float t){
			if (smoothMove)
				t = Mathf.SmoothStep (0, 1, t);
			objectToRotate.transform.localEulerAngles = Vector3.Lerp (init,  privateRotation, t);
			
		}
	}
}
