using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_FakeFence : MonoBehaviour {

	public float size;
	public float fadeDist = .7f;
	public Material mat;
	float dist;
	float dist2;
	Color white;
	// Use this for initialization
	void Start () {
		this.transform.localScale = new Vector3 (size, size, size);
		white = new Color (1, 1, 1, 1);
	}
	
	// Update is called once per frame
	void Update () {
		dist = Vector3.Distance (Camera.main.transform.position, Vector3.zero);
		dist2 = DLUtility.remap (dist, size - fadeDist, size, 0, .5f);
		white.a = dist2;
		mat.SetColor ("_Color", white);
	}
}
