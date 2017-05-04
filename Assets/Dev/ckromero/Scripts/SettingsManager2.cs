using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SettingsManager2 :  NetworkBehaviour {

	[SyncVar(hook ="OnChangeExperienceLength")]
	public float experienceLengthSeconds;
	[SyncVar]
	public float deathLengthSeconds;
	[SyncVar]
	public float faderLevelsMax;
	[SyncVar]
	public float bugPushStrength;
	[SyncVar]
	public float bugPullStrength;
	[SyncVar]
	public int resetHeadset = -2;
    [SyncVar]
    public string headsetText;

    public float timeMax;

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
        if (!isServer)
            return;

        experienceLengthSeconds = e;
        levelHandler.timeMax = e;
        Debug.Log(experienceLengthSeconds);
    }

	void Update(){

        if(experienceLengthSeconds!=timeMax)
            experienceLengthSeconds = timeMax;
		
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

