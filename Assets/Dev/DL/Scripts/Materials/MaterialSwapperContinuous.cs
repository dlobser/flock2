using UnityEngine;
using System.Collections;

public class MaterialSwapperContinuous : MonoBehaviour {

	//** made for shader: RGBRemapSimpleTransparentMultiTex

	public Texture[] textures;
	public int whichTexture;
	public SetMeshAttributes mesh;

	float fader = 0;
	public float speed = 1;
	float swapCounter = 0;

	public float count = 2;
	float counter = 0;

	Coroutine coSwap;

	public bool autoUpdate = false;


	void Update(){
		if (autoUpdate) {
			counter += Time.deltaTime * speed;
			if (counter > count) {
				counter = 0;
				swapMat ();
			}
		}
	}

	public void swapMat(){
		
		whichTexture++;
		if (whichTexture > 9)
			whichTexture = 1;
		fader = whichTexture - 1;

		float from = fader;
		StartCoroutine (Swap (from,(float)whichTexture));
//		Debug.Log ("Swap: "+from+","+ whichTexture);


//		StopCoroutine (Swap ());

	}

	public void swapMat(int which){
		int w = which;
		if (which > 9)
			w = 1;
		fader = w - 1;
		float from = fader;
		StartCoroutine (Swap (from,(float)w));
	}

	IEnumerator Swap(float moveTo, float moveFrom){

		while(swapCounter<1){
			swapCounter += Time.deltaTime * speed;
			fader = Mathf.Lerp (moveTo, moveFrom, swapCounter);
//			Debug.Log (fader);
			mesh.whichBug = fader;
			yield return new WaitForSeconds (Time.deltaTime);
		}
		fader = moveTo;
		swapCounter = 0;
		yield return fader;
	}

}
