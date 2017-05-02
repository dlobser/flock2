using UnityEngine;
using System.Collections;

public class FaderAudio : Fader {

	public AudioManager audi;
	public string[] tracks;
	int which = 0;
	public float transitionSpeed = 1;

	public override void Init(){
		levels = max - min;
		string thisTrack = tracks [0];
		audi.TransitionAudio (thisTrack, transitionSpeed, 1);
	}


	public override void Fade(){
		float currentLevel = (Mathf.Clamp (level, min, max) / levels) * tracks.Length;
		which = (int)Mathf.Clamp (Mathf.Floor (currentLevel), 0, tracks.Length - 1);
		string thisTrack = tracks [which];
		audi.TransitionAudio (thisTrack, transitionSpeed, 1);
	}
}
