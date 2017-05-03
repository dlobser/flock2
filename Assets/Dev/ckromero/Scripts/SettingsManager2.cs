using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SettingsManager2 :  NetworkBehaviour {

	[SyncVar]
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
	public int resetHeadset;

	public LevelHandler levelHandler;
	public ManageRigidBugs bugManagement;
	public TapToReset reset;
	public FaderManager fader;

	void Start () {
		resetHeadset = -2;
	}

	void Update(){
		levelHandler.timeMax = experienceLengthSeconds;
		levelHandler.timeStartDeathClock = experienceLengthSeconds - deathLengthSeconds;
		levelHandler.maxLevel = faderLevelsMax;
		bugManagement.pullForce = bugPullStrength;
		bugManagement.pushForce = bugPushStrength;
		fader.maxLevel = faderLevelsMax;

		if(resetHeadset>-2){
			Debug.Log ((Network.player.ToString ()));

			if (resetHeadset == int.Parse (Network.player.ToString ())) {
				reset.Reset();
				Debug.Log ("reset: " + resetHeadset);
			}
			resetHeadset = -2;
		}
	}
		
}

