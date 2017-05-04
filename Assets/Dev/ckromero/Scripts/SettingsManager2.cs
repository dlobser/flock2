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
  public bool resetHeadset = false;
  [SyncVar(hook = "OnChangeResetHeadsetImmediate")]
  public bool resetHeadsetImmediate = false;
  [SyncVar(hook="OnChangeHeadsetText")]
  public string headsetText;

  public float   ExperienceLengthSeconds = 420;
  public float   DeathLengthSeconds = 30;
  public float   FaderLevelsMax = 200;
  public float   BugPushStrength = .15f;
  public float   BugPullStrength = .1f;
  public bool    ResetHeadset;
  public bool ResetHeadsetImmediate;
  public string  HeadsetText;

  public LevelHandler levelHandler;
  public ManageRigidBugs bugManagement;
  public TapToReset reset;
  public FaderManager fader;
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

    public void OnChangeDeathLengthSeconds(float e) { DeathLengthSeconds = e; }
  public void OnChangeFaderLevelsMax(float e) { faderLevelsMax = e; }
  public void OnChangeBugPushStrength(float e) { bugPushStrength = e; }
  public void OnChangeBugPullStrength(float e) { bugPullStrength = e; }
  public void OnChangeResetHeadset(bool e) { resetHeadset = e; }
  public void OnChangeResetHeadsetImmediate(bool e) { resetHeadsetImmediate = e; }

  public void OnChangeHeadsetText(string e) { headsetText = e; }


  void Update(){
    if (Input.GetKeyUp(KeyCode.A))
    {
      //ResetHeadset = true;
      resetHeadset = true;
    }
    if (Input.GetKeyUp(KeyCode.B))
    {
     // ResetHeadsetImmediate = true;
      resetHeadset = true;
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
    }
		levelHandler.timeStartDeathClock = experienceLengthSeconds - deathLengthSeconds;
		levelHandler.maxLevel = faderLevelsMax;
		bugManagement.pullForce = bugPullStrength;
		bugManagement.pushForce = bugPushStrength;
		fader.maxLevel = faderLevelsMax;

        if (prevHeadsetText != headsetText)
            displayText.text = headsetText;

    if (resetHeadsetImmediate)
     {
     // ResetHeadsetImmediate = false;
      resetHeadsetImmediate = false;
      reset.Reset();
      fader.Refresh();
      Debug.Log("resetImmediate");
    }
    if (resetHeadset  && levelHandler.timer > experienceLengthSeconds ){
      // ResetHeadset = false;
      Debug.Log("reset");
      resetHeadset = false;
      reset.Reset();
      fader.Refresh();
		}

        prevHeadsetText = headsetText;
	}
		
}

