using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager :MonoBehaviour
{
	public AudioMixer audioMixer;

	private  string currentAudioMixerSnapshot;

	public void TransitionAudio(string amsToName, float timeForTransition, float weight=1.0f) { 
		AudioMixerSnapshot ams = audioMixer.FindSnapshot (amsToName);
		AudioMixerSnapshot[] amsArray = new AudioMixerSnapshot[]{ams};
		float[] weightArray = new float[]{ weight };

		audioMixer.TransitionToSnapshots(amsArray,weightArray,timeForTransition); 
		currentAudioMixerSnapshot=ams.name;
	}
	public string currentSnapshot(){
		if (currentAudioMixerSnapshot != null) {
			return currentAudioMixerSnapshot;
		} else {
			return "";
		}
	}




}


/* Audio Notes
 * heartbeat doesn't loop properly
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 */
