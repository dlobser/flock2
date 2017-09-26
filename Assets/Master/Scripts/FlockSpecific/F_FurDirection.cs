using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class F_FurDirection : MonoBehaviour {

	public Vector3 Velocity;
	public float multiplier = 1;
	Material furMaterial;
	// Use this for initialization
	void Start () {
		furMaterial = this.GetComponent<MeshRenderer> ().material;

	}
	
	// Update is called once per frame
	void Update () {
		furMaterial.SetVector("_Velocity", transform.InverseTransformDirection(Velocity*multiplier));

	}
}
