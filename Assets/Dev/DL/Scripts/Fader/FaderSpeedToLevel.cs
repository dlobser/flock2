using UnityEngine;
using System.Collections;

public class FaderSpeedToLevel : MonoBehaviour {

	GetSpeed getSpeed;
	FaderManager fader;
	// Use this for initialization
	void Start () {
		getSpeed = GetComponent<GetSpeed> ();
		fader = GetComponent<FaderManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		fader.level = getSpeed.averageSpeedOverThousand;
	}
}
