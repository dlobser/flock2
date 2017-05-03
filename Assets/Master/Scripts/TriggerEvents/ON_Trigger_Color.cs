using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{
	public class ON_Trigger_Color : ON_TriggerEvents {

	    public Color newColor;
	    public Color oldColor;
	    public bool getOldColorFromMaterial = false;
		public bool getOldColorFromCurrent = false;
	    public string channel;
	    public float speed;
	    float counter = 0;
	    Material mat;
	    public bool reverse = false;
		public bool pingpong;

		public MeshRenderer Renderer;

		void Awake(){
			if (Renderer == null)
				mat = this.GetComponent<MeshRenderer> ().material;
			else
				mat = Renderer.material;
			if(getOldColorFromMaterial)
				oldColor = mat.GetColor(channel);
		}

	    public override void Ping()
	    {
			    base.Ping ();
	        if(getOldColorFromCurrent)
	            oldColor = mat.GetColor(channel);
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
			if(!reverse)
				mat.SetColor(channel,Color.Lerp(oldColor, newColor, t));
			else
				mat.SetColor(channel, Color.Lerp(newColor, oldColor, t));
		}
	}
}
