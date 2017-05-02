using UnityEngine;
using System.Collections;

public class offsetUV : MonoBehaviour {

	public Vector2 speed;
	Vector2 off = Vector2.zero;
	Material mat;
	public string channel;
	public MeshRenderer rend;
	// Use this for initialization
	void Start () {
		if (rend == null)
			mat = GetComponent<Renderer> ().sharedMaterial;
		else
			mat = rend.material;
	}
	
	// Update is called once per frame
	void Update () {
		off += speed * Time.deltaTime;
		mat.SetTextureOffset (channel, off);
	}
}
