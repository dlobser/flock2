using UnityEngine;
using System.Collections;

public class FaderIndicatorColor : Fader {

	public Color[] _Color;
	Material mat;
	public GameObject indicator;

	void Start(){
		mat = indicator.GetComponent<MeshRenderer> ().material;
	}

	public override void Fade(){
		
		float currentLevel =( ( Mathf.Clamp( level,min,max)-min) / levels)*_Color.Length;
		Color thisColor = _Color [(int)Mathf.Clamp(Mathf.Floor (currentLevel),0,_Color.Length-1)];
		Color nextColor = _Color [(int)Mathf.Clamp(Mathf.Ceil (currentLevel),0,_Color.Length-1)];
		Color matColor = Color.Lerp (thisColor, nextColor, currentLevel - Mathf.Floor (currentLevel));
		if (matColor.a > 0) {
			if(!indicator.activeInHierarchy)
				indicator.SetActive(true);
			mat.SetColor ("_Color", matColor);
		} else if (indicator.activeInHierarchy) {
			indicator.SetActive (false);
		}

	}
}
