using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
			"'h' hides UI, 'r' resets selected headset, '0-8' selects headset, '9' selects all headsets" + "\n" + 
			"Headset Select: " + settings.whichHeadset + "\n" + 
			"Faders: " + (int)settings.faderLevelsMax + " \n " +
			"Length: " + (int)settings.experienceLengthSeconds + "\n " +
			"Pull: " + settings.bugPullStrength + " \n " +
			"Push: " + settings.bugPushStrength + " \n " +
			"DeathTime: " + (int)lHandler.timeStartDeathClock + " \n " +
			"Level: " + (int)lHandler.level + "\n" +

            "Time: " + lHandler.timer;
    }
}
