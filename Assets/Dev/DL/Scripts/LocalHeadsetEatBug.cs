using UnityEngine;
using System.Collections;

public class LocalHeadsetEatBug : MonoBehaviour {

	LevelHandler handler;

	void Start(){
		handler = GameObject.Find ("LevelHandler").gameObject.GetComponent<LevelHandler> ();
	}

	void OnTriggerEnter(Collider c){
		BugMVideo b = c.GetComponent<BugMVideo>();
		F_VomitMover v = c.GetComponent<F_VomitMover> ();
		if(b!=null){
			handler.EatBug ();
		}
		if (v != null) {
			for (int i = 0; i < 10; i++) {
				handler.EatBug ();
			}
		}
	}
}
