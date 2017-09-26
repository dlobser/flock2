using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_RandomUV : MonoBehaviour {

  Mesh mesh;
	// Use this for initialization
	void Start () {
    mesh = this.GetComponent<MeshFilter>().mesh;
    Vector2[] uv = new Vector2[mesh.uv.Length];
    float u = Random.value;
    float v = Random.value;
    for (int i = 0; i < uv.Length; i++)
    {
      uv[i] = new Vector2(u, v);
    }
    mesh.uv = uv;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
