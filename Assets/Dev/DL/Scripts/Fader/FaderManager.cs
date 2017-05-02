using UnityEngine;
using System.Collections;

public class FaderManager : MonoBehaviour {

	Fader[] faders;
	public float level;
	public float minLevel;
	public float maxLevel;

	float prevLevel = -1f;
	// Use this for initialization
	void Start () {
		faders = GetComponentsInChildren<Fader> ();
		Refresh ();
	}

	void Refresh(){
		for (int i = 0; i < faders.Length; i++) {
			faders [i].min = minLevel;
			faders [i].max = maxLevel;
			faders [i].Init ();
		}
	}
	// Update is called once per frame
	void Update () {
		if (prevLevel != level) {
			for (int i = 0; i < faders.Length; i++) {
				if (faders [i].min != minLevel || faders [i].max != maxLevel)
					Refresh ();
				faders [i].level = level;
				faders [i].Fade ();
			}
		}
		prevLevel = level;
	}
}
