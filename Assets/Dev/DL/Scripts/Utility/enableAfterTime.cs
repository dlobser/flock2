using UnityEngine;
using System.Collections;

public class enableAfterTime : MonoBehaviour {

	public float time;
	public SphereCollider sphere;
	float counter = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!sphere.enabled) {
			StartCoroutine (timeUp ());
		} else if (sphere.enabled) {
			StopCoroutine (timeUp ());
		}
	}

	IEnumerator timeUp(){
		counter += Time.deltaTime;
		if (counter > time) {
			sphere.enabled = true;
			counter = 0;
		}
		yield return new WaitForSeconds (Time.deltaTime);
	}
}
