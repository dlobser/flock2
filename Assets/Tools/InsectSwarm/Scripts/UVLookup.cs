using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UVLookup : MonoBehaviour {

//	public Vector2 UVLookup;
	public int X;
	public int Y;
	Vector2[] positions;
	int I = 1;
	int J = 0;

	public int start = 0;
	public Mesh mesh;
	public List<Vector2[]> UVList;

	Vector2[] initUV;

	void Awake(){
		Init ();
	}
		

	// Use this for initialization
	public void Init () {
		UVList = new List<Vector2[]>();
		initUV = new Vector2[mesh.uv.Length];
		for (int i = 0; i < initUV.Length; i++) {
			initUV [i] = mesh.uv [i];
		}
		setupPositions ();
		BuildLookup ();

	}

	void setupPositions(){
		positions = new Vector2[X * Y];
		for (int i = 0; i < positions.Length; i++) {
			positions [i] = new Vector2 (I, J);
			I++;
			if(I>X){
				I=1;
				J++;
				if(J>=Y){
					J=0;
				}
			}
		}
	}

	public void BuildLookup(){
		int count = start;
		for (int j = 0; j < positions.Length; j++) {
			
			Vector2[] nUV = new Vector2[mesh.uv.Length];

			Vector2 px = positions [count];

			count++;
			if (count > positions.Length - 1)
				count = 0;
			
			for (int i = 0; i < nUV.Length; i++) {
				float x = (initUV [i].x / (float)X) + (1f - (px.x / (float)X));
				float y = (initUV [i].y / (float)Y) + px.y / (float)Y;
				nUV [i] = new Vector2 (x, y);
			}

			UVList.Add (nUV);
		}
	}
}
