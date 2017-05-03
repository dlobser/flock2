using UnityEngine;
using System.Collections;

public class PlayerMove_NoRigidBody : MonoBehaviour
{

	public bool simulateMovement;
	public float movementMultiplier = 1;
	Vector3 average;
	public bool cullCorrection;
	Material furMaterial;

	Vector3 prevPosition;
	private Mesh mesh;
//	Camera cam;
	private Bounds originalBounds;
	public MeshRenderer rend;

	public float smoothing = 15;

    void Start()
    {
//		cam = Camera.main;

		if (rend == null && this.GetComponent<MeshRenderer> () != null)
			rend = this.GetComponent<MeshRenderer> ();
		else
			Debug.LogWarning ("missing renderer");
		
		if (GetComponent<MeshFilter> () != null) {
			mesh = GetComponent<MeshFilter> ().mesh;
			originalBounds = mesh.bounds;
		} else
			cullCorrection = false;

//		if (furMaterial == null && rend!=null)
			furMaterial = rend.material;
    }


    void Update()
    {

		//Set the shader's velocity
		if ( simulateMovement)
		{
			Vector3 diff = this.transform.position - prevPosition;
			diff *= movementMultiplier;
			average = average * smoothing;
			average += diff;
			average /= (smoothing+1);
			furMaterial.SetVector ("_Velocity", new Vector4 (average.x, average.y, average.z, 0));
			prevPosition = this.transform.position;
		}

		//Culling Correction Continued
		//Unity's Frustrum Culling will cull a mesh that has a vertex shader applied to it that scales its vertices larger than they are
		//If you find your fur dissapering near the edge of the screen, enable Cull Correction
		if (cullCorrection)
		{
			Vector3 camPosition = GetComponent<Camera>().transform.position;
			Vector3 normCamForward = Vector3.Normalize(GetComponent<Camera>().transform.forward);

			float boundsDistance = (GetComponent<Camera>().farClipPlane - GetComponent<Camera>().nearClipPlane) / 2 + GetComponent<Camera>().nearClipPlane;
			Vector3 boundsTarget = camPosition + (normCamForward * boundsDistance);
			Vector3 realtiveBoundsTarget = this.transform.InverseTransformPoint(boundsTarget);
			mesh.bounds = new Bounds(realtiveBoundsTarget, Vector3.one);
		}
		else if (mesh!=null)
		{
			mesh.bounds = originalBounds;
		}

    }

}
