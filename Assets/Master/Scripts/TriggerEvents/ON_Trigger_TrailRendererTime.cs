using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{
	public class ON_Trigger_TrailRendererTime : ON_TriggerEvents {

	    public float speed;
	    float counter = 0;
		public float maxTime;
        public float maxWidth;
		float[] init;
        float[] wInit;

		public TrailRenderer[] trailRenderers;

		void Start(){
			init = new float[trailRenderers.Length];
            wInit = new float[trailRenderers.Length];
            for (int i = 0; i < trailRenderers.Length; i++) {
				init [i] = trailRenderers [i].time;
                wInit [i] = trailRenderers[i].widthMultiplier;
            }
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
				Animate (counter);
	            yield return new WaitForSeconds(Time.deltaTime);
	        }
			Reset();
			yield return null;
	    }

		void Animate(float t){
			for (int i = 0; i < trailRenderers.Length; i++) {
				trailRenderers [i].time = Mathf.Lerp(init[i],maxTime,t);
                trailRenderers[i].widthMultiplier = Mathf.Lerp(wInit[i], maxWidth, t);
            }
		}
	}
}
