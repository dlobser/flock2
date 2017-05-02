using UnityEngine;
using System.Collections;

public class ShadowPositioner : MonoBehaviour {

	public float height;
	Vector3 pos;
//	SettingsManager settings;
	Color col;
	Vector3 scale;
	SpriteRenderer rend;
	public float shadowSize;
	public Color shadowColor;

	void Start(){
//		settings = GameObject.Find ("SettingsManager").GetComponent<SettingsManager> ();
		rend = this.GetComponent<SpriteRenderer> ();
	}
//	void Update(){
//		SetPosition ();
//	}
	// Update is called once per frame
	public void SetPosition (Vector3 vec) {
//		Debug.Log (shadowSize + " , " + shadowColor.a);
		if (shadowSize <= 0 || shadowColor.a <= 0 && rend.enabled) {
			rend.enabled = false;
		} else if (shadowSize >= 0 || shadowColor.a >= 0 && !rend.enabled)
			rend.enabled = true;
		if (shadowSize >= 0 && shadowColor.a >= 0) {
			pos.Set (vec.x, height, vec.z);
			this.transform.position = pos;
//			Debug.Log (this.transform.position);
			float size = shadowSize;
			scale.Set (size, size, size);
			this.transform.localScale = scale;
			rend.color = shadowColor;
		}
	}
}
//
//Vector4 colVec = settings.settingsJSON.shadowColor;
//col = new Color (colVec.x, colVec.y, colVec.z, colVec.w);
