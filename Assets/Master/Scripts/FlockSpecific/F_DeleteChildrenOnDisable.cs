using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_DeleteChildrenOnDisable : MonoBehaviour {

	List<GameObject> children;
	// Use this for initialization
	void Awake () {
		children = new List<GameObject> ();
		for (int i = 0; i < this.transform.childCount; i++) {
			children.Add (this.transform.GetChild (i).gameObject);
		}
	}
	
	// Update is called once per frame
	void OnDisable () {
		for (int i = 0; i < this.transform.childCount; i++) {
			GameObject.Destroy (children [i]);
		}
	}
}
