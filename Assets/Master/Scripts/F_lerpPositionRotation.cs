using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_lerpPositionRotation : MonoBehaviour {

    public GameObject target;
    public float speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (target != null)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, target.transform.position, speed);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, target.transform.rotation, speed);
        }
	}
}
