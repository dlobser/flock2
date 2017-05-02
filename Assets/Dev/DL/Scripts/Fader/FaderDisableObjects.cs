using UnityEngine;
using System.Collections;

public class FaderDisableObjects : Fader {

	public GameObject[] obj;
	int which = 0;

	public override void Fade(){
		float currentLevel = ((Mathf.Clamp (level, min, max)-min) / levels) * obj.Length;
		int which = (int)Mathf.Clamp (Mathf.Floor (currentLevel), 0, obj.Length - 1);
		for (int i = 0; i < obj.Length; i++) {
			if (obj [i] != null) {
				if (i <= which && obj [i].activeInHierarchy)
					obj [i].SetActive (false);
//				else
//					obj [i].SetActive (true);
			}
		}
	}
}
