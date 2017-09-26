using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_DeleteBasedOnHeight : MonoBehaviour {

	public float minHeight;
	public float maxHeight;
	public float scalar;

	void Update () {
		if (this.transform.position.y < maxHeight) {
			Vector3 tempScale = this.transform.localScale;
			tempScale.Scale(new Vector3(scalar, scalar, scalar));
			this.transform.localScale = tempScale;
		}
		if (this.transform.position.y < minHeight) {
			GameObject.Destroy (this.gameObject);
		}
	}
}
