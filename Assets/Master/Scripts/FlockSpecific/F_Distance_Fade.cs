using UnityEngine;
using System.Collections;

public class F_Distance_Fade : MonoBehaviour {

	public float nearDistance;
	public float farDistance;

	public float nearScale;
	public float farScale;

	public MeshRenderer rend;
	Material mat;

	public DistanceToCamera dist;
	public float scale;
	Color init;


	// Use this for initialization
	void Start () {
		if(dist==null)
			dist = GetComponent<DistanceToCamera> ();
		mat = rend.material;
		init = mat.color;
	}
	
	// Update is called once per frame
	void Update () {
		float distance = Mathf.Clamp( dist.distance, nearDistance,farDistance);
		distance = DLUtility.remap (distance, nearDistance, farDistance, 0, 1);
		scale = Mathf.Lerp (nearScale, farScale, distance );
		init.a = scale;
		mat.SetColor ("_Color", init);

//		Debug.Log (dist.distance);

	}

	float map(float s, float a1, float a2, float b1, float b2)
	{
		return b1 + (s-a1)*(b2-b1)/(a2-a1);
	}
}
