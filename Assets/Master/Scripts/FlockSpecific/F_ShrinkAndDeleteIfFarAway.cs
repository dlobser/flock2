using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_ShrinkAndDeleteIfFarAway : MonoBehaviour {

	public float speed;
	GameObject avatar;
	public float distance;
	bool shrinking = false;
	public Vector3 initScale;
	// Use this for initialization
	void Start () {
		avatar = GameObject.FindObjectOfType<F_IsLocalPlayer> ().gameObject;
//		initScale = this.transform.localScale;
	}

	void OnEnable(){
		this.transform.localScale = initScale;
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector2.Distance (new Vector2(this.transform.position.x,this.transform.position.z), new Vector2(avatar.transform.position.x,avatar.transform.position.z)) > distance && !shrinking) {
			shrinking = true;
			StartCoroutine (shrink ());
		}
	}

	IEnumerator shrink(){
		float counter = 0;
		while (counter < speed) {
			counter += Time.deltaTime;
			this.transform.localScale = Vector3.Lerp (initScale, Vector3.zero, counter / speed);
			yield return new WaitForSeconds (Time.deltaTime);
		}
		this.gameObject.SetActive (false);
	}
}
