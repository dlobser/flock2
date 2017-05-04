using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_AssignCamRigToMoveTrigger : MonoBehaviour {

    GameObject rig;
    bool found;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!found)
        {
            if (rig == null && GameObject.Find("[CameraRig](Clone)")!=null)
            {
                rig = GameObject.Find("[CameraRig](Clone)");
                found = true;
                GetComponent<ON.ON_Trigger_Move>().objectToMove = rig;
            }
        }
	}
}
