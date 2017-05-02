using UnityEngine;
using System.Collections;

public class F_Distance_FurBrightness : MonoBehaviour {

	public float nearDistance;
	public float farDistance;
	public float nearScale;
	public float farScale;
	public DistanceToCamera dist;
	public float scale;
	public string Channel;
	public Material mat;
	public GameObject objectWithMat;
	// Use this for initialization
	void Start () {
		if(dist==null)
			dist = GetComponent<DistanceToCamera> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		if (objectWithMat != null && objectWithMat.GetComponent<MeshRenderer> ().material)
			mat = objectWithMat.GetComponent<MeshRenderer> ().material;
		float distance = Mathf.Clamp( dist.distance, nearDistance,farDistance);
		distance -= nearDistance;
//		distance /= (farDistance);
		scale = Mathf.Lerp (nearScale, farScale, distance/(farDistance-nearDistance));
//		scale = Mathf.Max(farScale, Mathf.Min(nearScale, map (distance, nearDistance, farDistance, nearScale, farScale)));
		mat.SetFloat(Channel,scale);

	}

	float map(float s, float a1, float a2, float b1, float b2)
	{
		return b1 + (s-a1)*(b2-b1)/(a2-a1);
	}
}
