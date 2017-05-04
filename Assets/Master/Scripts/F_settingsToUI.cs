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
            "faders: " + settings.faderLevelsMax + " , " +
            "length: " + settings.experienceLengthSeconds + " , " +
            "pull: " + settings.bugPullStrength + " , " +
            "push: " + settings.bugPushStrength + " , " +
            "deathTime: " + lHandler.timeStartDeathClock + " , " +
            "level: " + lHandler.level + "\n" +

            "time: " + lHandler.timer;
    }
}
