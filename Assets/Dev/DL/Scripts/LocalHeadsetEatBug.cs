using UnityEngine;
using System.Collections;

public class LocalHeadsetEatBug : MonoBehaviour {

	LevelHandler handler;

	void Start(){
		handler = GameObject.Find ("LevelHandler").gameObject.GetComponent<LevelHandler> ();
	}

	void OnTriggerEnter(Collider c){
		BugMVideo b = c.GetComponent<BugMVideo>();
		if(b!=null ){
			handler.EatBug ();
		}
	}
}
