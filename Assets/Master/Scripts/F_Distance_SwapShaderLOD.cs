using UnityEngine;
using System.Collections;

public class F_Distance_SwapShaderLOD : MonoBehaviour {

	public float nearDistance;
	public float farDistance;

	public DistanceToCamera dist;
	public float scale;

	public bool deactivateNear;

	public Shader[] shaders;
	public Material[] mat;

	float prevDistance;


	// Use this for initialization
	void Start () {
		if(dist==null)
			dist = GetComponent<DistanceToCamera> ();
	}
	
	// Update is called once per frame
	void Update () {
		float distance = Mathf.Clamp( dist.distance, nearDistance,farDistance);

		if (distance != prevDistance) {

			distance -= nearDistance;
			int index = (int)Mathf.Min(shaders.Length-1,Mathf.Floor((distance / (farDistance - nearDistance))*shaders.Length));
//			Debug.Log (index);
			for (int i = 0; i < mat.Length; i++) {
				mat[i].shader = shaders [index];
			}

		}

		prevDistance = distance;

	}

	float map(float s, float a1, float a2, float b1, float b2)
	{
		return b1 + (s-a1)*(b2-b1)/(a2-a1);
	}
}
