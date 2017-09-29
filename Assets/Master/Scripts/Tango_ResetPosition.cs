using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tango_ResetPosition : MonoBehaviour {

	int taps;
	public float tapAmount = 3;
	float counter = 1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown) {
			if (taps == 0)
				counter = 0;
			taps++;

		}
		if(taps!=0)
			counter += Time.deltaTime;
		if (taps > tapAmount) {
			MoveParent ();
			taps = 0;
			counter = 0;
		}
		if (counter > 1)
			taps = 0;
//		Debug.Log (counter + " , " + taps);
		
	}

	void MoveParent(){
		Debug.Log ("move");
		GameObject g = this.transform.GetChild (0).gameObject;
		Matrix4x4 mat = g.transform.worldToLocalMatrix;
		transform.localPosition = mat.GetColumn( 3 );
		transform.localScale = new Vector3( mat.GetColumn( 0 ).magnitude, mat.GetColumn( 1 ).magnitude, mat.GetColumn( 2 ).magnitude );
		float w = Mathf.Sqrt( 1.0f + mat.m00 + mat.m11 + mat.m22 ) / 2.0f;
		transform.localRotation = new Quaternion( ( mat.m21 - mat.m12 ) / ( 4.0f * w ), ( mat.m02 - mat.m20 ) / ( 4.0f * w ), ( mat.m10 - mat.m01 ) / ( 4.0f * w ), w );
		g.transform.parent = null;
		transform.localPosition = Vector3.zero;
		transform.localEulerAngles = Vector3.zero;
		g.transform.parent = this.transform;
	}
}
