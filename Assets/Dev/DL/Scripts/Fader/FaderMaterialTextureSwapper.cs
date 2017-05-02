using UnityEngine;
using System.Collections;

public class FaderMaterialTextureSwapper : Fader {

	public Texture[] textures;
	public Material mat;
	public string textureName;
	int which = 0;
//	MeshRenderer rend;
	Vector2 range = Vector2.zero;

	void Start(){
//		rend = obj.GetComponent<MeshRenderer> ();
		setRange (level);
	}

	void setRange(float val){
		range = new Vector2 (Mathf.Floor (val) - .5f, Mathf.Floor (val) + 1);
	}


	public override void Fade(){
		if (level > range.y || level < range.x) {
			setRange (level);
			float currentLevel = ((Mathf.Clamp (level, min, max)-min) / levels) * textures.Length;
			Texture thisTex = textures [(int)Mathf.Clamp (Mathf.Floor (currentLevel), 0, textures.Length - 1)];
			mat.SetTexture (textureName, thisTex);
		}
	}
}
