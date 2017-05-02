using UnityEngine;
using System.Collections;

public class DistanceToCamera : MonoBehaviour {

	public float distance;
	// Use this for initialization

	// Update is called once per frame
	void Update () {
		distance = Vector3.Distance (Camera.main.transform.position, this.transform.position);
	}
}
