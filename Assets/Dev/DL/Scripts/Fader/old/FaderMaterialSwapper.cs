using UnityEngine;
using System.Collections;

public class FaderMaterialSwapper : Fader {

	public Material[] materials;
	public GameObject[] obj;
//	int which = 0;
	Vector2 range = Vector2.zero;

	void Start(){
		setRange (level);
	}

	void setRange(float val){
		range = new Vector2 (Mathf.Floor (val) - .5f, Mathf.Floor (val) + 1);
	}


	public override void Fade(){
		if (level > range.y || level < range.x) {
			setRange (level);
			float currentLevel = ((Mathf.Clamp (level, min, max)-min) / levels) * materials.Length;
			Material thisMat = materials [(int)Mathf.Clamp (Mathf.Floor (currentLevel), 0, materials.Length - 1)];
			for (int i = 0; i < obj.Length; i++) {
				obj[i].GetComponent<MeshRenderer>().material = thisMat;
			}
		}
	}
}
