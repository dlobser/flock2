using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_Funkify : MonoBehaviour {

  public GameObject top;
  public Mesh mesh;
  public Material mat;
	// Use this for initialization
	void Start () {
    for (int i = 0; i < top.transform.childCount; i++)
    {
      //Destroy(top.transform.GetChild(i).gameObject);
      //top.transform.GetChild(i + 1).transform.localPosition += Vector3.up * Random.value * 10f;
      top.transform.GetChild(i).GetComponent<MeshFilter>().mesh = mesh;
      top.transform.GetChild(i).GetComponent<MeshRenderer>().material = mat;
    }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
