using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{
	public class ON_Trigger_OffsetTexture : ON_TriggerEvents {

	    public float speed;
	    float counter = 0;
		public MeshRenderer renderer;
		Material mat;
		public string channel;
		public Vector2 offsetSpeed;
		Vector2 offset;

		public offsetUV uvOffset;

		void Start(){
			mat = renderer.material;
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
			uvOffset.speed = Vector2.Lerp (Vector2.zero, offsetSpeed, t);
		}
	}
}
