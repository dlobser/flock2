using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_TangoTurnOffHeadLerp : MonoBehaviour {

	public float checkFrequency;
	// Use this for initialization
	void Start () {
		StartCoroutine (TurnOffLerping ());

	}

	IEnumerator	TurnOffLerping(){

		yield return new WaitForSeconds (checkFrequency);
		Object_SyncPosition[] xForm = GameObject.FindObjectsOfType<Object_SyncPosition> ();
		for (int i = 0; i < xForm.Length; i++) {
			if (xForm [i].lerpToCamHeight)
				xForm [i].lerpToCamHeight = false;
		}
		StopCoroutine (TurnOffLerping ());
		StartCoroutine (TurnOffLerping ());
	}
}
