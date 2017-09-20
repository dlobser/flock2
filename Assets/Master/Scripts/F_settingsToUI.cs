using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class F_settingsToUI : MonoBehaviour {

    public SettingsManager2 settings;
    public LevelHandler lHandler;
    Text uiText;
	// Use this for initialization
	void Start () {
        uiText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
    uiText.text =
        "shift 'H' hides UI, shift 'R' resets selected headset(s),\n '0-8' selects headset, '9' selects all headsets" + "\n" +
        "This Player ID: " + settings.ThisPlayer + "\n" +

        "Headset Select: " + settings.whichHeadset + "\n" + 
        "Faders: " + (int)settings.faderLevelsMax + " \n " +
        "Total Length: " + (int)settings.experienceLengthSeconds + "\n " +
        "Death Length: " + (int)settings.deathLengthSeconds + " \n " +
        "Death Start Time: " + (int)lHandler.timeStartDeathClock + " \n " +
        "Death Count: " + lHandler.deathCount + " \n " +
        "Death Clock: " + lHandler.deathClock + "\n" +
        "Pull: " + settings.bugPullStrength + " \n " +
        "Push: " + settings.bugPushStrength + " \n " +
        "Level: " + (int)lHandler.level + "\n" +
        "Max Time: " + (int)lHandler.timeMax + "\n" +
        "Fence Size: " + settings.fenceSize + "\n" +
        "Time: " + (int)lHandler.timer;
    }
}
