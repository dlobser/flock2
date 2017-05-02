using UnityEngine;
using System.Collections;

public class FaderMaterialVector : Fader {

	public Vector4[] _Vectors;
	public Material mat;
	public string vectorName;

	public override void Fade(){
		float currentLevel =(( Mathf.Clamp( level,min,max)-min) / levels)*_Vectors.Length;
		Vector4 thisVec = _Vectors [(int)Mathf.Clamp(Mathf.Floor (currentLevel),0,_Vectors.Length-1)];
		Vector4 nextVec = _Vectors [(int)Mathf.Clamp(Mathf.Ceil (currentLevel),0,_Vectors.Length-1)];

		Vector4 matVec = Vector4.Lerp (thisVec, nextVec, currentLevel - Mathf.Floor (currentLevel));
		mat.SetVector (vectorName, matVec);
	}
}
