using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class FaderText : Fader {

	public string[] text;
	public TextMesh display;

	public override void Fade(){
		
		float currentLevel = ((Mathf.Clamp (level, min, max)-min) / levels) * text.Length;
		string thisText = text [(int)Mathf.Clamp (Mathf.Floor (currentLevel), 0, text.Length - 1)];
		if (!display.gameObject.GetComponent<MeshRenderer> ().enabled && thisText.Length>0)
			display.gameObject.GetComponent<MeshRenderer> ().enabled = true;
		display.text = thisText;
	}
}
