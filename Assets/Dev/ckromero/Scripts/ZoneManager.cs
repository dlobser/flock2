using UnityEngine;
using System.Collections;

public class ZoneManager : MonoBehaviour {
	public string currentActorZone="nowhere";
	public float exitZoneTime=2.0f;
	private string updatedZone;
	private float lastUpdate=0.0f;

	void Update() { 
		if (Time.time - lastUpdate > exitZoneTime) { 
			currentActorZone="nowhere";	
		}
	}

	public void UpdateCurrentPlayerZone(string name)  { 
		currentActorZone = name;
		if (name == "dyingZone") {
//			Debug.Log ("in " + name);
		}
		lastUpdate = Time.time;
		
	}
}
