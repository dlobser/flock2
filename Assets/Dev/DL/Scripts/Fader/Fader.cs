using UnityEngine;
using System.Collections;

public class Fader : MonoBehaviour {

	public float level { get; set; }
	public float max { get; set; }
	public float min { get; set; }
	public float levels{ get; set; }

	public virtual void Init(){
		levels = max - min;
	}

	public virtual void Fade(){
	}

}
