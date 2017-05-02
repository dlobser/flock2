using UnityEngine;
using System.Collections;

public class FaderDeathHeadset : Fader {

//	public Holojam.Tools.Viewer viewer;
	public Vector3 floatTo;
	public float levelToFloat = 3;
	Vector3 initPosition;
	public float speed = .001f;
	float counter = 0;
	// Use this for initialization

	// Update is called once per frame
	public override void Fade(){
		if (level > levelToFloat) {
//			if (viewer.isActiveAndEnabled) {
//				viewer.enabled = false;
//				initPosition = viewer.transform.position;
				StartCoroutine (floatHeadset ());
//			}
		}
	}

	IEnumerator floatHeadset(){

		while (!initPosition.Equals (floatTo)) {
			counter += Time.deltaTime*speed;
//			viewer.transform.position = Vector3.Lerp (initPosition, floatTo, counter);
			yield return new WaitForSeconds (Time.deltaTime);
		}
	}
}
