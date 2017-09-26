using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_ScaleOnActivation : MonoBehaviour {

	public Vector3 start;
	public Vector3 end;
	public float speed;

	void OnEnable(){
		StartCoroutine (scale ());
	}

	IEnumerator scale(){
		float count = 0;
		while (count < speed) {

			count += Time.deltaTime;
			this.transform.localScale = Vector3.Lerp (start, end, Mathf.SmoothStep(0,1, count / speed));
			yield return new WaitForSeconds (Time.deltaTime);
		}
		yield return null;
	}
}
