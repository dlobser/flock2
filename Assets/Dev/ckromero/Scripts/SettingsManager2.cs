using UnityEngine;
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
  public int resetHeadset = -2;
  [SyncVar(hook="OnChangeHeadsetText")]
  public string headsetText;

  public float   ExperienceLengthSeconds = 420;
  public float   DeathLengthSeconds = 30;
  public float   FaderLevelsMax = 200;
  public float   BugPushStrength = .15f;
  public float   BugPullStrength = .1f;
  public int     ResetHeadset = -2;
  public string  HeadsetText;

  public LevelHandler levelHandler;
  public ManageRigidBugs bugManagement;
  public TapToReset reset;
  public FaderManager fader;
  public TextMesh displayText;
  string prevHeadsetText;

  void Start () {
	  resetHeadset = -2;
        if (isServer)
        {
            Debug.Log("i'm server");
        }
  }

    public void OnChangeExperienceLength(float e)
    {
        experienceLengthSeconds = e;
        levelHandler.timeMax = e;
    }

    public void OnChangeDeathLengthSeconds(float e) { DeathLengthSeconds = e; }
  public void OnChangeFaderLevelsMax(float e) { faderLevelsMax = e; }
  public void OnChangeBugPushStrength(float e) { bugPushStrength = e; }
  public void OnChangeBugPullStrength(float e) { bugPullStrength = e; }
  public void OnChangeResetHeadset(int e) { resetHeadset = e; }
  public void OnChangeHeadsetText(string e) { headsetText = e; }


  void Update(){

    if (isServer)
    {
      experienceLengthSeconds = ExperienceLengthSeconds;
      deathLengthSeconds = DeathLengthSeconds;
      faderLevelsMax = FaderLevelsMax;
      bugPushStrength = BugPushStrength;
      bugPullStrength = BugPullStrength;
      resetHeadset = ResetHeadset;
      headsetText = HeadsetText;
      experienceLengthSeconds = ExperienceLengthSeconds;
    }
		levelHandler.timeStartDeathClock = experienceLengthSeconds - deathLengthSeconds;
		levelHandler.maxLevel = faderLevelsMax;
		bugManagement.pullForce = bugPullStrength;
		bugManagement.pushForce = bugPushStrength;
		fader.maxLevel = faderLevelsMax;

        if (prevHeadsetText != headsetText)
            displayText.text = headsetText;

		if(resetHeadset>-2){
			Debug.Log ((Network.player.ToString ()));

			if (resetHeadset == int.Parse (Network.player.ToString ())) {
				reset.Reset();
				Debug.Log ("reset: " + resetHeadset);
			}
			resetHeadset = -2;
		}

        prevHeadsetText = headsetText;
	}
		
}

