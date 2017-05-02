using UnityEngine;
using System.Collections;

public class FaderObjectVisibility : Fader {

	public GameObject[] obj;
	int which = 0;

	public override void Fade(){
		float currentLevel = ( (Mathf.Clamp (level, min, max)-min) / levels) * obj.Length;
		int which = (int)Mathf.Clamp (Mathf.Floor (currentLevel), 0, obj.Length - 1);
		GameObject thisObj = obj [which];
		for (int i = 0; i < obj.Length; i++) {
			if (i != which)
				obj [i].SetActive (false);
		}
		if(!thisObj.activeInHierarchy)
			thisObj.SetActive (true);
	}
}
