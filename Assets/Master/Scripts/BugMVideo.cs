using UnityEngine;
using System.Collections;
//using Holojam.Tools;

public class BugMVideo : MonoBehaviour{
	public float scaleSpeed = .1f;
	public int collisionWindow=2;

	Renderer[] renderers;
	ManageRigidBugs bb;
	Vector3 scalar = Vector3.one;

	public string label;

	float scale;
	float initScale;
	int active = 1;
	bool doneScalingDown = false;
	bool doneScalingUp = false;
//	bool collisionAllowed;
//	bool materialSwapAllow = false;

//	int prevEmit = -1;
//	int emit;

	public string avatarName = "BirdAvatar";

	public int id {get;set;}

	void Awake(){
//		emit = 0;
//		collisionAllowed = true;
		bb = GameObject.Find("BugManager").GetComponent<ManageRigidBugs>();
		renderers = GetComponentsInChildren<Renderer> ();
		scale = this.transform.localScale.x;
		initScale = scale;
	}

	void ActivateDeactivateRenderer(){
		foreach (Renderer r in renderers) {
			r.enabled = active==1;
		}
	}

	//Nothing below here executes on the client.
	void OnTriggerEnter(Collider c){
		if(c.name == "Viewer" || c.name==avatarName){
			bb.SendMessage("ProcessCollision", this); //Callback
			bb.SendMessage("SwapTexture", this); //Callback
			bb.SendMessage("LevelUp", this);
			StartCoroutine(DisableThis());
		}
	}

	//trying to prevent multiple collisions against the same object. 
	//Could also try looking at the same objects within a given window.
	IEnumerator CloseCollisionWindow() { 
//		collisionAllowed = false;
		yield return new WaitForSeconds (collisionWindow);
//		collisionAllowed = true;
	}

	IEnumerator DisableThis(){
		while(!doneScalingDown){
			if (scale > .001f) {
				scale = Mathf.MoveTowards(scale, 0f, scaleSpeed);
				scalar.Set (scale, scale, scale);
				this.transform.localScale = scalar;
				yield return null;
			} else
				doneScalingDown = true;
		}
		active = 0;
		doneScalingDown = false;
        this.transform.localScale = Vector3.zero;
		yield return null;//new WaitForSeconds(bb.disableTime);
		bb.SendMessage ("ResetPosition", this);
//		materialSwapper.swapMat ();
		active = 1;
//		emit = 0;
        this.transform.localScale = Vector3.zero;
		while (!doneScalingUp) {
			if (scale < initScale) {
				scale = Mathf.MoveTowards (scale, initScale, scaleSpeed);
				scalar.Set (scale, scale, scale);
				this.transform.localScale = scalar;
				yield return null;
			} else
				doneScalingUp = true;
		}
		doneScalingUp = false;
		scale = initScale;
	}
}