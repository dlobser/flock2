using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_HideChild : MonoBehaviour {
  bool onOff;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    if (Input.GetKeyUp(KeyCode.H))
    {
      onOff = true;
    }
    if (onOff)
    {
      if (this.transform.GetChild(0).gameObject.activeInHierarchy)
        this.transform.GetChild(0).gameObject.SetActive(false);
      else
        this.transform.GetChild(0).gameObject.SetActive(true);
      onOff = false;
    }
	}
}
