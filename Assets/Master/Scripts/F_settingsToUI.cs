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
			"faders: " + settings.faderLevelsMax + " \n " +
			"length: " + settings.experienceLengthSeconds + "\n " +
			"pull: " + settings.bugPullStrength + " \n " +
			"push: " + settings.bugPushStrength + " \n " +
			"deathTime: " + lHandler.timeStartDeathClock + " \n " +
            "level: " + lHandler.level + "\n" +

            "time: " + lHandler.timer;
    }
}
