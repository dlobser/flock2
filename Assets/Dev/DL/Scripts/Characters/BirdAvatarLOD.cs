using UnityEngine;
using System.Collections;

public class BirdAvatarLOD : MonoBehaviour {

	public SkinnedMeshRenderer[] skins;
	public GameObject[] wings;
	public float slider;
	public float flapSpeed = 0;
	public float flapAmount = 0;
	float flapCount = 0;
	Vector3 scalar = Vector3.one;
	Vector3 leftWingRotation = Vector3.zero;
	Vector3 rightWingRotation = Vector3.zero;
	bool wingsActive = true;
	DistanceToCamera dist;
	float distToCam = 0;
	public Vector4 distances;
	public bool manualSet = false;
	private float slide = 0;
//
//	public float[] LODs;
//	bool[] trueLODs;
//	bool[] LODDid;

	// Use this for initialization
	void Start () {
			flapCount = Random.value * 10f;
		dist = this.transform.GetChild(0).GetComponent<DistanceToCamera> ();
//		trueLODs = bool[LODs.Length];
//		LODDid = bool[LODs.Length];
	}
	
	// Update is called once per frame
	void Update () {
		Slide ();
	}

//	public void checkLOD(){
//		for (int i = 0; i < LODs-1; i++) {
//			if (slide > LODs [i] && slide < LODs [i + 1]){
//				if (trueLODs [i] = false)
//					LODDid [i] = true;
//				trueLODs [i] = true;
//
//			}
//			else{
//				trueLODs [i] = false;
//			}
//		}
//	}

	public void DeactivateWings(){
		for (int i = 0; i < wings.Length; i++) {
			wings [i].GetComponent<MeshRenderer> ().enabled = false;
//			wings [i].transform.GetChild(0).GetComponent<SpriteRenderer> ().enabled = false;
		}
		wingsActive = false;
	}
	public void ActivateWings(){
		for (int i = 0; i < wings.Length; i++) {
			wings [i].GetComponent<MeshRenderer> ().enabled = true;
		}
		wingsActive = true;
	}


//	public void LowResWings(){
//		for (int i = 0; i < wings.Length; i++) {
//			wings [i].GetComponent<MeshRenderer> ().enabled = false;
//			wings [i].transform.GetChild(0).GetComponent<SpriteRenderer> ().enabled = true;
//		}
//		wingsActive = false;
//	}
//	public void HighResWings(){
//		for (int i = 0; i < wings.Length; i++) {
//			wings [i].GetComponent<MeshRenderer> ().enabled = true;
//			wings [i].transform.GetChild(0).GetComponent<SpriteRenderer> ().enabled = false;
//
//		}
//		wingsActive = true;
//	}

	public void LowResBody(){

	}

	public void HighResBody(){

	}


	public void Slide(){
		slide = slider;
		if (!manualSet) {
			distToCam = dist.distance;
			slide = DLUtility.remap (distToCam, distances.x, distances.y, distances.z, distances.w);
		}
		for (int i = 0; i < skins.Length; i++) {
			skins [i].SetBlendShapeWeight (0, slide * 100);
		}

//		if (LODDid [0]) {
//			HighResWings ();
//			LODDid [0] = false;
//		}
//		else if (LODDid [1]) {
//			LowResWings ();
//			LODDid [1] = false;
//		}
//		else if (LODDid [2]) {
//			Deacti ();
//			LODDid [1] = false;
//		}
		if (slide >= 1 && wingsActive) {
			DeactivateWings ();
		} else if (slide < .5f && !wingsActive) {
			ActivateWings ();
		}


		if (wingsActive) {
			float newSlide = Mathf.Clamp (1 - slide, 0, 1);
			flapCount += Time.deltaTime * flapSpeed;
			scalar.Set (newSlide, newSlide, newSlide);
			leftWingRotation.Set (0, 0, Mathf.Sin (flapCount) * flapAmount);
			rightWingRotation.Set (0, 0, Mathf.Sin (-flapCount) * flapAmount);
			for (int i = 0; i < wings.Length; i++) {
				wings [i].transform.localScale = scalar;
			}
			wings [0].transform.localEulerAngles = leftWingRotation;
			wings [1].transform.localEulerAngles = rightWingRotation;
		}
	}
}
