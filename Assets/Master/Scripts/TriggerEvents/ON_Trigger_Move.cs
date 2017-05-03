using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{
	public class ON_Trigger_Move : ON_TriggerEvents {

	    public Vector3 move;
		private Vector3 initialMove;
		private Vector3 privateMove;
		private Vector3 init;
		public bool randomize = false;
		public bool smoothMove = false;
		public bool localPosition = false;
		public bool relativePosition = false;
		public float timeToMove = 1;

		public GameObject objectToMove;

		float counter = 0;
//		bool triggered = false;

		public override void OnEnable(){
			base.OnEnable ();
			GetComponent<ON_InteractableEvents> ().OnIdle += Idle;

			if (objectToMove == null)
				objectToMove = this.gameObject;
			initialMove = move;
			privateMove = move;

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
			if(localPosition)
				privateMove = relativePosition ? objectToMove.transform.localPosition + initialMove : privateMove;
			else
				privateMove = relativePosition ? objectToMove.transform.position + initialMove : privateMove;
			GetInitialPosition ();
		}

		void GetInitialPosition(){
			if (localPosition)
				init = objectToMove.transform.localPosition;
			else
				init = objectToMove.transform.position;

			if (randomize) {

				privateMove = move;
				privateMove.Scale (Random.insideUnitSphere);

				if(localPosition)
					privateMove+=objectToMove.transform.localPosition ;
				else
					privateMove+=objectToMove.transform.position ;

			
			}
		
		}

		IEnumerator HandlePing(){
			while (counter < timeToMove) {
				counter += Time.deltaTime;
				Animate (counter / timeToMove);
				yield return null;
			}
			Reset ();
		}

		void Animate(float t){
			if (smoothMove)
				t = Mathf.SmoothStep (0, 1, t);
			if (localPosition) 
				objectToMove.transform.localPosition = Vector3.Lerp (init, privateMove , t);
			else
				objectToMove.transform.position = Vector3.Lerp (init,  privateMove, t);
			
		}
	}
}
