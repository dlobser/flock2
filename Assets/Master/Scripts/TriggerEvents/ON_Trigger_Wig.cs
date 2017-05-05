using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{
	public class ON_Trigger_Wig : ON_TriggerEvents {

	    public float speed;
		public float lengthMax;
	    float counter = 0;
		public GameObject Wig;

		public DistanceToCamera distance;
		private F_Distance_FurBrightness wigDist;

		Kvant.WigController wigCtrl;
		Material wigMat;
		F_CopyXForms copyXForms;

		void Start(){
			if (Wig == null)
				Wig = GameObject.Find ("Wig");
			wigCtrl = Wig.GetComponent<Kvant.WigController> ();
			wigMat = Wig.GetComponent<MeshRenderer> ().material;
			copyXForms = wigCtrl.target.transform.parent.gameObject.GetComponent<F_CopyXForms> ();
			wigDist = Wig.GetComponent<F_Distance_FurBrightness> ();
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
			triggered = false;
			copyXForms.target = null;
			Wig.GetComponent<MeshRenderer> ().enabled = false;
		}
	

	    IEnumerator HandlePing()
	    {
			counter = 0;
	        while (counter < speed)
	        {
	            counter += Time.deltaTime;
//				Debug.Log ("Example: " + counter);
	            yield return new WaitForSeconds(Time.deltaTime);
	        }
			Reset();
			yield return null;
	    }

		void Animate(float t){
			if (!triggered && t > 0) {
				triggered = true;
				copyXForms.target = this.transform;
				copyXForms.ForceUpdate ();
				Wig.GetComponent<Kvant.WigController> ().ResetSimulation ();
				wigDist.dist = distance;
				Wig.GetComponent<MeshRenderer> ().enabled = true;

			}
//			if(t>.1f && Wig.GetComponent<MeshRenderer> ().enabled ==false && copyXForms.target!=null)
//				Wig.GetComponent<MeshRenderer> ().enabled = true;
//			if (copyXForms.target == null)
//				Wig.GetComponent<MeshRenderer> ().enabled = false;
//				
			float dist = Vector3.Distance (this.transform.position, Camera.main.transform.position);
			dist -= 1;
			dist *= -1;
			dist = Mathf.Clamp (0, 1, dist);
			wigCtrl.length = t*lengthMax*dist;

//			Debug.Log ("Example: " + t);
		}
	}
}
