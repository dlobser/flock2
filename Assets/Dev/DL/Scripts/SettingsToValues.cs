using UnityEngine;
using System.Collections;

public class SettingsToValues : MonoBehaviour {

//	public GameObject bugManager;
//	public GameObject actorManager;
//	public LevelHandler levelHandler;
//	public TextMesh text;
//
//	BugManager bugManagement;
//	Bug[] bugs;
//
//	Holojam.Tools.Actor[] actors;
//	SettingsManager settings;
//	SettingsJSON prev;
//	Holojam.Tools.Holobounds holobounds;
//
//	FindEmptyCoordinate findEmpty;
//
//	FaderManager fader;
//
//
//
//	// Use this for initialization
//	void Start () {
//		findEmpty = GameObject.Find ("GridCheck").GetComponent<FindEmptyCoordinate> ();
//		fader = GameObject.Find ("LevelFader").GetComponent<FaderManager> ();
//		holobounds = GameObject.Find ("Holobounds").GetComponent<Holojam.Tools.Holobounds>();
//		settings = GameObject.Find ("SettingsManager").GetComponent<SettingsManager> ();
//		bugs = bugManager.GetComponentsInChildren<Bug> ();
//		bugManagement = bugManager.GetComponent<BugManager> ();
//		actors = actorManager.GetComponentsInChildren<Holojam.Tools.Actor> ();
//		prev = new SettingsJSON ();
////		propertyCopy (settings.settingsJSON,prev);
//	}
//
//	// Update is called once per frame
//	void Update () {
//		if (prev.bugColliderSize != 
//			settings.settingsJSON.bugColliderSize||
//			prev.bugMaxSize != settings.settingsJSON.bugMaxSize||
//			prev.bugMinSize != settings.settingsJSON.bugMinSize) {
//			UpdateBug ();
//		}
//		if (prev.bugDeathTime != settings.settingsJSON.bugDeathTime) {
//			UpdateBugManager ();
//		}
//		if (prev.birdColliderSize != settings.settingsJSON.birdColliderSize||
//			prev.birdMaxHeadSize != settings.settingsJSON.birdMaxHeadSize||
//			prev.birdMinHeadSize != settings.settingsJSON.birdMinHeadSize ||
//			prev.birdNearDistance!=settings.settingsJSON.birdNearDistance||
//			prev.birdFarDistance!=settings.settingsJSON.birdFarDistance) {
//			UpdateBird ();
//		}
//		if (prev.Holobound1 != settings.settingsJSON.Holobound1||
//			prev.Holobound2 != settings.settingsJSON.Holobound2||
//			prev.Holobound3 != settings.settingsJSON.Holobound3||
//			prev.Holobound4 != settings.settingsJSON.Holobound4){
//			UpdateHolobound ();
//		}
//		if (prev.faderLevelsMax != settings.settingsJSON.faderLevelsMax||
//			prev.faderLevelsMin != settings.settingsJSON.faderLevelsMin) {
//			UpdateFaderManager ();
//		}
//		if (prev.experienceLengthSeconds != settings.settingsJSON.experienceLengthSeconds||
//			prev.graceTime != settings.settingsJSON.graceTime ||
//			prev.deathLengthSeconds != settings.settingsJSON.deathLengthSeconds) {
//			UpdateLevelHandler ();
//		}
//		if (prev.globalHeadsetUIMessage != settings.settingsJSON.globalHeadsetUIMessage) {
//			UpdateText ();
//		}
//		propertyCopy (settings.settingsJSON,prev);
//	}
//
//	public void UpdateLevelHandler(){
//		levelHandler.timeMax = settings.settingsJSON.experienceLengthSeconds;
//		levelHandler.hungryTime = settings.settingsJSON.graceTime;
//		levelHandler.timeStartDeathClock = levelHandler.timeMax - settings.settingsJSON.deathLengthSeconds;
//
//	}
//
//	public void UpdateBug(){
//		for (int i = 0; i < bugs.Length; i++) {
//			bugs [i].GetComponent<SphereCollider> ().radius = settings.settingsJSON.bugColliderSize;
//			bugs [i].transform.GetChild (0).GetComponent<ScaleToDistance> ().nearScale = settings.settingsJSON.bugMaxSize;
//			bugs [i].transform.GetChild (0).GetComponent<ScaleToDistance> ().farScale = settings.settingsJSON.bugMinSize;
//
//
//		}
//	}
//	public void UpdateBird(){
//		for (int i = 0; i < actors.Length; i++) {
//			actors [i].GetComponent<SphereCollider> ().radius = settings.settingsJSON.birdColliderSize;
//			actors [i].transform.GetChild(0).GetComponent<ScaleToDistance> ().nearScale = settings.settingsJSON.birdMaxHeadSize;
//			actors [i].transform.GetChild(0).GetComponent<ScaleToDistance> ().farScale = settings.settingsJSON.birdMinHeadSize;
//			actors [i].transform.GetChild (0).GetComponent<ScaleToDistance> ().nearDistance = settings.settingsJSON.birdNearDistance;
//			actors [i].transform.GetChild (0).GetComponent<ScaleToDistance> ().farDistance = settings.settingsJSON.birdFarDistance;
//		}
//	}
//
//	public void UpdateFaderManager(){
//		fader.maxLevel = settings.settingsJSON.faderLevelsMax;
//		fader.minLevel = settings.settingsJSON.faderLevelsMin;
//	}
//
//	public void UpdateBugManager(){
//		bugManagement.disableTime = settings.settingsJSON.bugDeathTime;
//	}
//
//	public void UpdateText(){
//		text.text = settings.settingsJSON.globalHeadsetUIMessage;
//		if (!text.GetComponent<MeshRenderer> ().enabled && text.text.Length > 0)
//			text.GetComponent<MeshRenderer> ().enabled = true;
//		else if(text.GetComponent<MeshRenderer> ().enabled && text.text.Length ==0)
//			text.GetComponent<MeshRenderer> ().enabled = false;
//	}
//	public void UpdateHolobound(){
//		holobounds.bounds [0] = settings.settingsJSON.Holobound1;
//		holobounds.bounds [1] = settings.settingsJSON.Holobound2;
//		holobounds.bounds [2] = settings.settingsJSON.Holobound3;
//		holobounds.bounds [3] = settings.settingsJSON.Holobound4;
//		holobounds.GetComponent<Holojam.Tools.Fence> ().Rebuild ();
//		findEmpty.Rebuild = true;
//	}
//
//	void propertyCopy(SettingsJSON From, SettingsJSON To){
//		To.deathLengthSeconds = From.deathLengthSeconds;
////		To.allowedSessionTime = From.allowedSessionTime;
//		To.Holobound1 = From.Holobound1;
//		To.Holobound2 = From.Holobound2;
//		To.Holobound3 = From.Holobound3;
//		To.Holobound4 = From.Holobound4;
//		To.birdColliderSize = From.birdColliderSize;
//		To.birdMaxHeadSize = From.birdMaxHeadSize;
//		To.birdMinHeadSize = From.birdMaxHeadSize;
//		To.birdNearDistance = From.birdNearDistance;
//		To.birdFarDistance = From.birdFarDistance;
//		To.bugColliderSize = From.bugColliderSize;
//		To.bugDeathTime = From.bugDeathTime;
//		To.bugMaxSize = From.bugMaxSize;
//		To.emitFromHead = From.emitFromHead;
//		To.emitFromFarthestQuadrant = From.emitFromFarthestQuadrant;
//		To.emitFromInitialGrid = From.emitFromInitialGrid;
//		To.experienceLengthSeconds = From.experienceLengthSeconds;
//		To.faderLevelsMax = From.faderLevelsMax;
//		To.faderLevelsMin = From.faderLevelsMin;
//
//		To.graceTime = From.graceTime;
//		To.globalHeadsetUIMessage = From.globalHeadsetUIMessage;
//		To.maxSpeedToSitStill = From.maxSpeedToSitStill;
//		To.slowMessage = From.slowMessage;
//		To.shadowColor = From.shadowColor;
//		To.shadowSize = From.shadowSize;
//		To.timeToDieMessage = From.timeToDieMessage;
//		To.timeLeftToDie = From.timeLeftToDie;
//		To.warnForSeconds = From.warnForSeconds;
//		To.wingLineRendererLength = From.wingLineRendererLength;
//	}
}
