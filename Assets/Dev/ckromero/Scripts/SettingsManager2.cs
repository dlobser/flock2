using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

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
	[SyncVar(hook="OnChangePauseHeadset")]
	public bool pauseHeadset = false;

    [SyncVar(hook="OnChangeHeadsetText")]
    public string headsetText;
    [SyncVar(hook="OnChangeWhichHeadset")]
    public int whichHeadset;
    [SyncVar(hook = "OnChangeFenceSize")]
    public float fenceSize = 2.5f;

    public float   ExperienceLengthSeconds = 420;
    public float   DeathLengthSeconds = 30;
    public float   FaderLevelsMax = 200;
    public float   BugPushStrength = .15f;
    public float   BugPullStrength = .1f;
    public bool    ResetHeadset;
    public bool    ResetHeadsetImmediate;
	public bool    PauseHeadset;
    public string  HeadsetText;

    public int WhichHeadset;
    public int ThisPlayer { get; set; }
    public float FenceSize = 2.5f;

    public LevelHandler levelHandler;
    public ManageRigidBugs bugManagement;
    public TapToReset reset;
	public TapToReset pause;
    public FaderManager[] fader;
    public TextMesh displayText;
    string prevHeadsetText;

    public GameObject[] hideIfClient;

    public Text headsetTextInput;

    public GameObject fence;
  
    void Start () {

        if (isServer)
        {
            Debug.Log("i'm server");
        }
        else {
            for (int i = 0; i < hideIfClient.Length; i++) {
                hideIfClient[i].SetActive(false);
            }
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
	public void OnChangePauseHeadset(bool e) { pauseHeadset = e;  }
    public void OnChangeResetHeadsetImmediate(bool e) { resetHeadsetImmediate = e;  }
    public void OnChangeWhichHeadset(int e) { whichHeadset = e;  }
    public void OnChangeFenceSize(float e) { fenceSize = e; }

    public void onChangeExperienceLength(float e){ExperienceLengthSeconds = e; levelHandler.timeMax = e; }
    public void onChangeDeathLengthSeconds(float e) { DeathLengthSeconds = e;}
    public void onChangeFaderLevelsMax(float e) { FaderLevelsMax = e; }
    public void onChangeBugPushStrength(float e) { BugPushStrength = e; }
    public void onChangeBugPullStrength(float e) { BugPullStrength = e;  }
    public void onChangeFenceSize(float e) { FenceSize = e; }

    public void OnChangeHeadsetText(string e) { headsetText = e; }
    public void onChangeHeadsetText(string e) { HeadsetText = e; }

    
    void Update(){
        if (Input.GetKeyUp(KeyCode.A))
        {
            //ResetHeadset = true;
        }
     
        if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.LeftShift))
        {
            ResetHeadsetImmediate = true;
        }

		if (Input.GetKey(KeyCode.P) && Input.GetKey(KeyCode.LeftShift))
		{
			PauseHeadset = true;
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
        if (Input.GetKeyUp (KeyCode.Alpha6)) {
            WhichHeadset = 6;
        }
        if (Input.GetKeyUp(KeyCode.Alpha7))
        {
            WhichHeadset = 7;
        }
        if (Input.GetKeyUp(KeyCode.Alpha8))
        {
            WhichHeadset = 8;
        }
        if (Input.GetKeyUp (KeyCode.Alpha9)) {
            WhichHeadset = -1;
        }
        if (isServer) {
            experienceLengthSeconds = ExperienceLengthSeconds;
            deathLengthSeconds = DeathLengthSeconds;
            faderLevelsMax = FaderLevelsMax;
            bugPushStrength = BugPushStrength;
            bugPullStrength = BugPullStrength;
            resetHeadset = ResetHeadset;
            resetHeadsetImmediate = ResetHeadsetImmediate;
			pauseHeadset = PauseHeadset;
            headsetText = headsetTextInput.text;
            experienceLengthSeconds = ExperienceLengthSeconds;
            whichHeadset = WhichHeadset;
            fenceSize = FenceSize;
            displayText.text = headsetTextInput.text;
        }
        else{
            displayText.text = headsetText;
        }

        if (fenceSize != fence.transform.localScale.x){
            fence.transform.localScale = new Vector3(fenceSize,fenceSize, fenceSize);
            fence.GetComponent<F_FakeFence>().size = fenceSize;
        }

        levelHandler.timeStartDeathClock = experienceLengthSeconds - deathLengthSeconds;
        levelHandler.maxLevel = faderLevelsMax;
        levelHandler.timeMax = ExperienceLengthSeconds;
        bugManagement.pullForce = bugPullStrength;
        bugManagement.pushForce = bugPushStrength;

        fader[0].maxLevel = faderLevelsMax;
        
        ThisPlayer = F_Players.thisPlayerID;
   
        if (resetHeadsetImmediate){
            //Debug.Log (F_Players.thisPlayerID);
            if (whichHeadset == F_Players.thisPlayerID || whichHeadset == -1) {
                levelHandler.timer = 0;
                levelHandler.deathClock = 0;
                levelHandler.level = 0;
                reset.Reset ();
                foreach (FaderManager fade in fader) {
                    fade.Refresh ();
                    fade.level = 0;
                }
                StartCoroutine(resetHeadsetText());
            }
            ResetHeadsetImmediate = false;
            ResetHeadsetImmediate = false;
        }
		if (pauseHeadset) {
			//Debug.Log (F_Players.thisPlayerID);
			if (whichHeadset == F_Players.thisPlayerID || whichHeadset == -1) {
				pause.Reset ();
			}
			PauseHeadset = false;
		}
        if (resetHeadset  && levelHandler.timer > experienceLengthSeconds ){
            ResetHeadset = false;
            Debug.Log("reset");
            levelHandler.timer = 0;
            levelHandler.deathClock = 0;
            levelHandler.level = 0;
            reset.Reset();
            foreach (FaderManager fade in fader) {
                fade.Refresh ();
                fade.level = 0;
            }
        }

        prevHeadsetText = headsetText;
    }

    IEnumerator resetHeadsetText() {
        Material mat = displayText.GetComponent<MeshRenderer>().material;
        Color col = mat.color;
        float counter = 0;
        headsetTextInput.transform.parent.GetComponent<InputField>().text = "Begin Flocking!";
        while (counter < 1) {
            counter += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        counter = 0;
        while (counter < 1) {
            counter += Time.deltaTime;
            mat.color = new Color(col.r, col.g, col.b, 1 - counter);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        headsetTextInput.transform.parent.GetComponent<InputField>().text = "";
        counter = 0;
        while (counter < .1f) {
            counter += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        mat.color = new Color(col.r, col.g, col.b, col.a);
    }

}

