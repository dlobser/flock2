using UnityEngine;
using System.Collections;

public class SetMeshAttributes : MonoBehaviour {

	public Color ColorR;
	public Color ColorG;
	public Color ColorB;
	public float colorCycleSpeed;
	public float whichBug;
	Vector2[] UV2;
	Vector2[] UV3;
	Vector2[] UV4;
	Mesh mesh;
	// Use this for initialization
	void Start () {
		mesh = GetComponent<MeshFilter> ().mesh;
    Set();
	}
	
	// Update is called once per frame
//	void Update () {
//		Set ();
//	}

	public void Set(){

		Vector2[] uv2 = new Vector2[mesh.uv.Length];
		Vector2[] uv3 = new Vector2[mesh.uv.Length];
		Vector2[] uv4 = new Vector2[mesh.uv.Length];
		Color[] colorsR = new Color[mesh.vertices.Length];

		float[] v = new float[] {
			ColorR.r, ColorR.g, ColorR.b,
			ColorG.r, ColorG.g, ColorG.b,
			ColorB.r, ColorB.g, ColorB.b
		};

		for (int i = 0; i < mesh.vertices.Length; i++) {
			uv2 [i] = new Vector2 (whichBug, v[8]);
			uv3 [i] = new Vector2 (v [0], v [1]);
			uv4 [i] = new Vector2 (v [2], v [3]);
			colorsR[i] = new Color(v[4],v[5],v[6],v[7]);

//			uv3 [i] = UV3;
		}

//		Debug.Log (Mathf.Floor (whichBug * 1000) + (colorCycleSpeed / 10f));

		mesh.uv2 = uv2;
		mesh.uv3 = uv3;
		mesh.uv4 = uv4;
		mesh.colors = colorsR;


	}

	Vector3 colToVec(Color col){
		return new Vector3 (col.r, col.g, col.b);
	}

	Vector3 colToVec4(Color col){
		return new Vector4 (col.r, col.g, col.b, col.a);
	}
}
