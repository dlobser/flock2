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
	    if (Input.GetKeyUp(KeyCode.H) && Input.GetKey(KeyCode.LeftShift))
	    {
	      onOff = true;
	    }
	    if (onOff)
	    {
			if (this.transform.GetChild (0).gameObject.activeInHierarchy)
				HideChildren (false);
			else
				HideChildren (true);
	      onOff = false;
	    }
	}

	void HideChildren(bool which){
		for (int i = 0; i < this.transform.childCount; i++) {
			this.transform.GetChild(i).gameObject.SetActive(which);
		}
	}
}
