using UnityEngine;
using System.Collections;

public class lookAtCam : MonoBehaviour {

	public bool constrainY = false;
	public float addRotation = 0;
	public bool onStartOnly;
	// Use this for initialization
	void Start () {
		if (onStartOnly)
			Look ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!onStartOnly)
			Look ();
	}

	void Look(){
		if (Camera.main) {
			transform.LookAt (Camera.main.transform.position);
			if (constrainY)
				transform.localEulerAngles = Vector3.Scale (transform.localEulerAngles, new Vector3 (0, 1, 0));
			transform.Rotate (0, addRotation, 0);
		}
	}
}
