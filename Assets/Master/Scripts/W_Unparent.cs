using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONTempleATrip { 
    public class W_Unparent : MonoBehaviour {

	    // Use this for initialization
	    void Start () {
            this.transform.parent = null;
            this.transform.position = Vector3.zero;
            this.transform.localEulerAngles = Vector3.zero;
	    }
	
    }
}