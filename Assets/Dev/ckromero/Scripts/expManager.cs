using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class expManager : MonoBehaviour {
	//GLOBAL SETTINGS
	public bool debugOn;

	//ENVIRONMENT SETTINGS
	public Environment environment;
	public bool environmentStatus=true;

	//AUDIO SETTINGS
	public AudioMixerGroup Master;
	public bool muteAudio;
	private float storedVolume;
	private float startVolume;
	private const float NOVOLUME = -80.0f;

	//ZONES
	public ZoneManager zonemanager;

	//PlayerStateManager
//	public PlayerStateManager playerStateManager;



//	public MommaBird mommabird;
//	public Food food;
//	public ActionTree actionTree;
//	public Mirror mirror;
//	public DeathBed deathbed;
//	public GestureManager gestureManager;
//	public MatingSpot matingSpot;
//	public CliffsEdge cliffsEdge;

	// Use this for initialization
	void Start () {
		//TODO: Make singleton
		Master.audioMixer.GetFloat("MasterVolume",out startVolume);	
	}
	
	// Update is called once per frame
	void Update () {
		//ENVIRONMENT
		if (environmentStatus == false) {
			environment.environmentContainer.gameObject.SetActive (false);
		} else {
			environment.environmentContainer.gameObject.SetActive (true);
		}


		//AUDIO
		if (debugOn) Debug.Log (storedVolume.ToString ());

		if (muteAudio == true) {

			Master.audioMixer.SetFloat ("MasterVolume", NOVOLUME);
		} else {
			Master.audioMixer.SetFloat("MasterVolume",startVolume);
		}
	}
}
