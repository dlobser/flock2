using UnityEngine;
using System.Collections;

public class F_Distance_Deactivate : MonoBehaviour {

	public float nearDistance;
	public float farDistance;

	public DistanceToCamera dist;
	public float scale;

	public bool deactivateNear;

	public GameObject[] toDeactivate;

	enum state {NEAR,FAR,MIDDLE};
	state currentState;

	// Use this for initialization
	void Start () {
		if(dist==null)
			dist = GetComponent<DistanceToCamera> ();
	}
	
	// Update is called once per frame
	void Update () {
		float distance = Mathf.Clamp( dist.distance, nearDistance,farDistance);

		if (distance.Equals (nearDistance) && deactivateNear && currentState!=state.NEAR) {
			for (int i = 0; i < toDeactivate.Length; i++) {
				toDeactivate [i].SetActive (false);
			}
			currentState = state.NEAR;
		} else if (distance.Equals (farDistance) && !deactivateNear && currentState!=state.FAR) {
			for (int i = 0; i < toDeactivate.Length; i++) {
				toDeactivate [i].SetActive (false);
			}
			currentState = state.FAR;
		} else if (distance > nearDistance && distance < farDistance && currentState != state.MIDDLE) {
			for (int i = 0; i < toDeactivate.Length; i++) {
				toDeactivate [i].SetActive (true);
			}
			currentState = state.MIDDLE;
		}
			

	}

	float map(float s, float a1, float a2, float b1, float b2)
	{
		return b1 + (s-a1)*(b2-b1)/(a2-a1);
	}
}
