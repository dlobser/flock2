using UnityEngine;
using System.Collections;

public class FaderAudioVolumes : Fader {

	public GameObject[] obj;
	int which = 0;

	public override void Fade(){
		float currentLevel = (Mathf.Clamp (level, min, max) / levels) * obj.Length;
		int which = (int)Mathf.Clamp (Mathf.Floor (currentLevel), 0, obj.Length - 1);
		for (int i = 0; i < obj.Length; i++) {
			if (i <= which)
				obj [i].GetComponent<AudioSource> ().volume = 1;
			else
				obj [i].GetComponent<AudioSource> ().volume = 0;
		}
	}
}
