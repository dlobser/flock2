using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{
	public class ON_Trigger_MaterialFloat : ON_TriggerEvents {

		public float newValue;
		public float oldValue;
		public bool getOldValueFromMaterial = false;
		public bool getOldValueFromCurrent = false;
		public string channel;
		public float speed;
		float counter = 0;
		Material mat;
		public bool reverse = false;
		public bool pingpong;
		public bool smoothStep;

		public MeshRenderer Renderer;

		void Start(){
			if (Renderer == null && this.GetComponent<MeshRenderer>()!=null)
				mat = this.GetComponent<MeshRenderer> ().material;
			else
				mat = Renderer.material;
			if(getOldValueFromMaterial)
				oldValue = mat.GetFloat(channel);
		}

		public override void Ping()
		{
			base.Ping ();
			if(getOldValueFromCurrent)
				oldValue = mat.GetFloat(channel);
			StartCoroutine(animate());

		}

		public override void Ping(float t)
		{
			ChangeColor (t);
		}

		public override void Reset(){
			base.Reset ();
		}


		IEnumerator UnAnimate()
		{
			while (counter > 0)
			{
				counter -= Time.deltaTime;
				ChangeColor (counter / speed);
				yield return new WaitForSeconds(Time.deltaTime);
			}
			Reset ();
			yield return null;
		}

		IEnumerator animate()
		{
			counter = 0;
			while (counter < speed)
			{
				counter += Time.deltaTime;
				ChangeColor (counter / speed);
				yield return new WaitForSeconds(Time.deltaTime);
			}
			if(pingpong)
				StartCoroutine(UnAnimate ());
			else
				Reset();
			yield return null;
		}

		void ChangeColor(float t){
			if (smoothStep)
				t = Mathf.SmoothStep (0, 1, t);
			if(!reverse)
				mat.SetFloat(channel,Mathf.Lerp(oldValue, newValue, t));
			else
				mat.SetFloat(channel, Mathf.Lerp(oldValue, newValue, t));
		}
	}
}
