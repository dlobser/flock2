using UnityEngine;
using System.Collections;

public class ZoneCollider : MonoBehaviour {

	private ZoneManager zoneManager;

	void Start() { 
		zoneManager = (ZoneManager) GetComponentInParent<ZoneManager> ();
	}
//	protected override void OnTriggerEnter(Collider other) {
//
//		if (Time.time > 1) {
////			base.OnTriggerEnter (other);
//
////			Debug.Log (this.name + " collided with " + other.name + " at: " + Time.time);
//		}
//	}
	private void OnTriggerStay(Collider other) { 
		if (other.name.Contains("Build")){
//			Debug.Log (this.name + " is still colliding with " + other.name + " at: " + Time.time);

			zoneManager.UpdateCurrentPlayerZone (this.name);
		}

	}
}
