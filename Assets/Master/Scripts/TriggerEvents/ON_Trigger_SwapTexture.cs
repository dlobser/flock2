using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{
	public class ON_Trigger_SwapTexture : ON_TriggerEvents {

		public Texture[] textures;
		public string channel;
		Material mat;
		int which = 0;
		public MeshRenderer Renderer;

		void Awake(){
			if (Renderer == null)
				mat = this.GetComponent<MeshRenderer> ().material;
			else
				mat = Renderer.material;
		}

		public override void Ping()
		{
			base.Ping ();
			ChangeColor ();

		}

		public override void Ping(float t)
		{
			Debug.LogWarning("this one should just be on enter or exit");
		}

		public override void Reset(){
			base.Reset ();
		}


	
		void ChangeColor(){
			
			mat.SetTexture(channel,textures[which]);
			which++;
			if (which > textures.Length-1)
				which = 0;

		}
	}
}
