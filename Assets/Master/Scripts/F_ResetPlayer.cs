using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_ResetPlayer : MonoBehaviour {

  public GameObject PlayerController;
  GameObject cam;
	// Use this for initialization
	void Start () {
    cam = Camera.main.gameObject;
	}

  public void Reset()
  {
    if(PlayerController!=null)
      DestroyImmediate(PlayerController);
     cam.SetActive(true);
  }
	

}
