﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SettingsManager2 :  NetworkBehaviour {

  [SyncVar(hook ="OnChangeExperienceLength")]
  public float experienceLengthSeconds;
  [SyncVar(hook="OnChangeDeathLengthSeconds")]
  public float deathLengthSeconds;
  [SyncVar(hook="OnChangeFaderLevelsMax")]
  public float faderLevelsMax;
  [SyncVar(hook="OnChangeBugPushStrength")]
  public float bugPushStrength;
  [SyncVar(hook="OnChangeBugPullStrength")]
  public float bugPullStrength;
  [SyncVar(hook="OnChangeResetHeadset")]
  public bool resetHeadset = false;
  [SyncVar(hook = "OnChangeResetHeadsetImmediate")]
  public bool resetHeadsetImmediate = false;
  [SyncVar(hook="OnChangeHeadsetText")]
  public string headsetText;
	[SyncVar(hook="OnChangeWhichHeadset")]
	public int whichHeadset;

  public float   ExperienceLengthSeconds = 420;
  public float   DeathLengthSeconds = 30;
  public float   FaderLevelsMax = 200;
  public float   BugPushStrength = .15f;
  public float   BugPullStrength = .1f;
  public bool    ResetHeadset;
  public bool ResetHeadsetImmediate;
  public string  HeadsetText;
	public int WhichHeadset;

  public LevelHandler levelHandler;
  public ManageRigidBugs bugManagement;
  public TapToReset reset;
  public FaderManager[] fader;
  public TextMesh displayText;
  string prevHeadsetText;
  
  void Start () {

        if (isServer)
        {
            Debug.Log("i'm server");
        }
 
    Debug.Log("Network Player#: " +  (Network.player.ToString()));
    
  }

    public void OnChangeExperienceLength(float e)
    {
        experienceLengthSeconds = e;
        levelHandler.timeMax = e;
    }

  public void OnChangeDeathLengthSeconds(float e) { deathLengthSeconds = e;}
  public void OnChangeFaderLevelsMax(float e) { faderLevelsMax = e; }
  public void OnChangeBugPushStrength(float e) { bugPushStrength = e; }
  public void OnChangeBugPullStrength(float e) { bugPullStrength = e;  }
  public void OnChangeResetHeadset(bool e) { resetHeadset = e;  }
  public void OnChangeResetHeadsetImmediate(bool e) { resetHeadsetImmediate = e;  }
	public void OnChangeWhichHeadset(int e) { whichHeadset = e;  }

	public void onChangeExperienceLength(float e){ExperienceLengthSeconds = e;}
	public void onChangeDeathLengthSeconds(float e) { DeathLengthSeconds = e;}
	public void onChangeFaderLevelsMax(float e) { FaderLevelsMax = e; }
	public void onChangeBugPushStrength(float e) { BugPushStrength = e; }
	public void onChangeBugPullStrength(float e) { BugPullStrength = e;  }
//	public void onChangeBugPullStrength(float e) { BugPullStrength = e;  }

  //public void OnChangeDeathLengthSeconds2(float e) { DeathLengthSeconds = e; }
  //public void OnChangeFaderLevelsMax(float e) { faderLevelsMax = e; FaderLevelsMax = e; }
  //public void OnChangeBugPushStrength(float e) { bugPushStrength = e; BugPushStrength = e; }
  //public void OnChangeBugPullStrength(float e) { bugPullStrength = e; BugPullStrength = e; }
  //public void OnChangeResetHeadset(bool e) { resetHeadset = e; ResetHeadset = e; }
  //public void OnChangeResetHeadsetImmediate(bool e) { resetHeadsetImmediate = e; ResetHeadsetImmediate = e; }

  public void OnChangeHeadsetText(string e) { headsetText = e; }


  void Update(){
    if (Input.GetKeyUp(KeyCode.A))
    {
      ResetHeadset = true;
      //resetHeadset = true;
    }
    if (Input.GetKeyUp(KeyCode.B))
    {
      ResetHeadsetImmediate = true;
      //resetHeadset = true;
    }

		if (Input.GetKeyUp (KeyCode.Alpha0)) {
			WhichHeadset = 0;
		}
		if (Input.GetKeyUp (KeyCode.Alpha1)) {
			WhichHeadset = 1;
		}
		if (Input.GetKeyUp (KeyCode.Alpha2)) {
			WhichHeadset = 2;
		}
		if (Input.GetKeyUp (KeyCode.Alpha3)) {
			WhichHeadset = 3;
		}
		if (Input.GetKeyUp (KeyCode.Alpha4)) {
			WhichHeadset = 4;
		}
		if (Input.GetKeyUp (KeyCode.Alpha5)) {
			WhichHeadset = 5;
		}
    if (isServer)
    {
      experienceLengthSeconds = ExperienceLengthSeconds;
      deathLengthSeconds = DeathLengthSeconds;
      faderLevelsMax = FaderLevelsMax;
      bugPushStrength = BugPushStrength;
      bugPullStrength = BugPullStrength;
      resetHeadset = ResetHeadset;
      resetHeadsetImmediate = ResetHeadsetImmediate;
      headsetText = HeadsetText;
      experienceLengthSeconds = ExperienceLengthSeconds;
			whichHeadset = WhichHeadset;
    }
		levelHandler.timeStartDeathClock = experienceLengthSeconds - deathLengthSeconds;
		levelHandler.maxLevel = faderLevelsMax;
		bugManagement.pullForce = bugPullStrength;
		bugManagement.pushForce = bugPushStrength;

		fader[0].maxLevel = faderLevelsMax;

        if (prevHeadsetText != headsetText)
            displayText.text = headsetText;

	if (resetHeadsetImmediate)
	{
			Debug.Log (F_Players.thisPlayerID);
		if (whichHeadset == F_Players.thisPlayerID) {


				levelHandler.timer = 0;
				//resetHeadsetImmediate = false;
				reset.Reset ();
				foreach (FaderManager fade in fader) {
					fade.Refresh ();
					fade.level = 0;
				}

				//      fader.Refresh();
				Debug.Log ("resetImmediate");
			}
			ResetHeadsetImmediate = false;
			ResetHeadsetImmediate = false;
	}
    if (resetHeadset  && levelHandler.timer > experienceLengthSeconds ){
      ResetHeadset = false;
      Debug.Log("reset");
			levelHandler.timer = 0;
      //resetHeadset = false;
      reset.Reset();
			foreach (FaderManager fade in fader) {
				fade.Refresh ();
				fade.level = 0;
			}
//      fader.Refresh();
		}

        prevHeadsetText = headsetText;
	}
		
}

