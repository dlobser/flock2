using UnityEngine;
using System.Collections;

public class FaderFogColor : Fader {

	public Color[] _Color;
	public bool interpolate = true;

	public override void Fade(){
		float currentLevel =( (Mathf.Clamp( level,min,max)-min) / levels)*_Color.Length;
		Color thisColor = _Color [(int)Mathf.Clamp(Mathf.Floor (currentLevel),0,_Color.Length-1)];
		if (interpolate) {
			Color nextColor = _Color [(int)Mathf.Clamp (Mathf.Ceil (currentLevel), 0, _Color.Length - 1)];
			Color matColor = Color.Lerp (thisColor, nextColor, currentLevel - Mathf.Floor (currentLevel));
			RenderSettings.fogColor = matColor;
			Camera.main.backgroundColor = matColor;
		} else {
			RenderSettings.fogColor=thisColor;
			Camera.main.backgroundColor = thisColor;
		}
	}
}
