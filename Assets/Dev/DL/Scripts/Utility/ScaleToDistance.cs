using UnityEngine;
using System.Collections;

public class ScaleToDistance : MonoBehaviour {

	public float nearDistance;
	public float farDistance;
	public float nearScale;
	public float farScale;
	public DistanceToCamera dist;
	public float scale;
	Vector3 Scalar = Vector3.one;
	// Use this for initialization
	void Start () {
		if(dist==null)
			dist = GetComponent<DistanceToCamera> ();
	}
	
	// Update is called once per frame
	void Update () {
		float distance = Mathf.Clamp( dist.distance, nearDistance,farDistance);
		distance -= nearDistance;
//		distance /= (farDistance);
		scale = Mathf.Lerp (nearScale, farScale, distance/(farDistance-nearDistance));
//		scale = Mathf.Max(farScale, Mathf.Min(nearScale, map (distance, nearDistance, farDistance, nearScale, farScale)));
		Scalar.Set (scale, scale, scale);
		this.transform.localScale = Scalar;
	}

	float map(float s, float a1, float a2, float b1, float b2)
	{
		return b1 + (s-a1)*(b2-b1)/(a2-a1);
	}
}
