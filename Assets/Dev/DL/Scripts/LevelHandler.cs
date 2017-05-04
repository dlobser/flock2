using UnityEngine;
using System.Collections;

public class LevelHandler : MonoBehaviour {

	public float timeMax;
	public float timeStartDeathClock;
	public float timer { get; set; }
	public float level;
	public float eatSpeed;
	public float levelDelta { get; set; }
	public float bugsEaten;
	public float hungryTime = 10;

	public float maxLevel = 30;

	public FaderManager[] BugsEaten;
	public FaderManager[] Level;
	public FaderManager[] LerpLevel;
	public FaderManager[] LevelDelta;
	public FaderManager[] DeathCount;

    public GameObject DeathFader;

	float deathClock;
	float deathCount;
	float lerpLevel = 0;
	float hungerTimer;
	float reduceLevelCounter = 0;

	bool PingPong;
	public bool eatBug;

    public bool debug;

	// Update is called once per frame

	void Update () {
		
		hungerTimer += Time.deltaTime;
		if (hungerTimer > hungryTime) {
			ReduceLevel ();
		}

		levelDelta = level - lerpLevel;
		lerpLevel = Mathf.MoveTowards (lerpLevel, level, .1f);

		timer += Time.deltaTime;


		if (timer > timeStartDeathClock && timer < timeMax) {
            if (DeathFader != null && DeathFader.activeInHierarchy == false)
                DeathFader.SetActive(true);
			deathClock += Time.deltaTime;
			deathCount = Mathf.Min(1.0f,deathClock / (timeMax - timeStartDeathClock));
		}

		if (eatBug) {
			EatBug ();
			eatBug = false;
		}

        if (debug)
        {
            Debug.Log(deathClock);
        }

		UpdateFaders ();

	}

	void ReduceLevel(){
		
		if (level > 0) {
			reduceLevelCounter += Time.deltaTime;
			if (reduceLevelCounter >= 1) {
				reduceLevelCounter = 0;
				level -= 1;
			}
		}

	}

	public void EatBug(){
		
		bugsEaten++;

		if (level < maxLevel && PingPong)
			level++;
		else 
			PingPong = false;
		if (level > 0 && !PingPong)
			level--;
		else
			PingPong = true;
			
		hungerTimer = 0;
	}

	public void Reset(){
		level = 0;
		deathClock = 0;
		deathCount = 0;
		timer = 0;
		bugsEaten = 0;
		lerpLevel = 0;
		hungerTimer = 0;
		level = 0;
    foreach (FaderManager fader in BugsEaten)
    {
      fader.Refresh();
    }
    foreach (FaderManager fader in Level)
    {
      fader.Refresh();
    }
    foreach (FaderManager fader in LerpLevel)
    {
      fader.Refresh();
    }
    foreach (FaderManager fader in LevelDelta)
    {
      fader.Refresh();
    }
    foreach (FaderManager fader in DeathCount)
    {
      fader.Refresh();
    }
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
		foreach (FaderManager fader in DeathCount) {
	    fader.level = deathCount;
		}

	}


}
