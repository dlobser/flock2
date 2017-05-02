using UnityEngine;
using System.Collections;

public class LevelHandler2 : MonoBehaviour {

	//Received from PlayerStateManager
	public float bugsEaten;
	public float percentInCurrentLevel;
	public float percentOfTotal;

	public float timeMax;
	public float timeStartDeathClock;
	public float timer { get; set; }
	public float level;
	public float eatSpeed;
	float lerpLevel = 0;
	public float levelDelta { get; set; }
	public float hungryTime = 10;
	float hungerTimer;

	public FaderManager[] BugsEaten;
	public FaderManager[] Level;
	public FaderManager[] LerpLevel;
	public FaderManager[] LevelDelta;
	
	// Update is called once per frame
	void Update () {
		hungerTimer += Time.deltaTime;
		if (hungerTimer > hungryTime) {
			ReduceLevel ();
		}
		lerpLevel = Mathf.MoveTowards (lerpLevel, level, .01f);
		levelDelta = level - lerpLevel;
		timer += Time.deltaTime;
		UpdateFaders ();
	}

	void ReduceLevel(){
		if(level>0)
			level -= Time.deltaTime;
	}

	public void EatBug(){
		bugsEaten++;
		level++;
		hungerTimer = 0;
	}

	void Debugger(){
		Debug.Log ("level: " + level);
		Debug.Log ("levelDelta: " + levelDelta);
		Debug.Log ("bugsEaten: " + bugsEaten);
		Debug.Log ("lerpLevel: " + lerpLevel);
		Debug.Log ("hungerTimer: " + hungerTimer);
	}

	void UpdateFaders(){

		foreach (FaderManager fader in BugsEaten) {
			fader.level = bugsEaten;
		}
		foreach (FaderManager fader in Level) {
			fader.level = level;
		}
		foreach (FaderManager fader in LerpLevel) {
			fader.level = lerpLevel;
		}
		foreach (FaderManager fader in LevelDelta) {
			fader.level = levelDelta;
		}

	}


}
