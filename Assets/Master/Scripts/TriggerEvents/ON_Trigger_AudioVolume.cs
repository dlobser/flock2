using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{
	public class ON_Trigger_AudioVolume : ON_TriggerEvents {

	    public float speed;
	    float counter = 0;
		public AudioSource[] audi;
		public float maxVolume = 1;
		public float minVolume = 0;

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
			foreach(AudioSource aud in audi)
				aud.volume = minVolume;
			counter = 0;
		}
	

	    IEnumerator HandlePing()
	    {
			counter = 0;
	        while (counter < speed)
	        {
	            counter += Time.deltaTime;
	            yield return new WaitForSeconds(Time.deltaTime);
	        }
			Reset();
			yield return null;
	    }

		void Animate(float t){
			foreach(AudioSource aud in audi)
				aud.volume = Mathf.Lerp (minVolume, maxVolume, t);
		}
	}
}
