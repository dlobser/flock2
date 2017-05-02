using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShadowsCtrl : MonoBehaviour {

//	List<ShadowPositioner> shadows;
//	List<Transform> actors;
//	GameObject actorManager;
//	public GameObject shadow;
//	SettingsManager settings;
//	Color col;
//	// Use this for initialization
//	void Start () {
//		settings = GameObject.Find ("SettingsManager").GetComponent<SettingsManager> ();
//		actorManager = GameObject.Find ("ActorManager");
//		shadows = new List<ShadowPositioner> ();
//		actors = new List<Transform> ();
//		for (int i = 0; i < actorManager.transform.childCount; i++) {
//			Transform t = actorManager.transform.GetChild (i);
//			if (!t.gameObject.GetComponent<Holojam.Tools.Actor>().isBuild) {
//				ShadowPositioner sh = Instantiate (shadow).GetComponent<ShadowPositioner>();
//				sh.transform.parent = this.transform;
//				shadows.Add (sh);
//				actors.Add (t);
//			}
//		}
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		for (int i = 0; i < shadows.Count; i++) {
//			shadows [i].shadowSize = settings.settingsJSON.shadowSize;
//			Vector4 colVec = settings.settingsJSON.shadowColor;
//			col.r = colVec.x;
//			col.g = colVec.y;
//			col.b = colVec.z;
//			col.a = colVec.w;
//			shadows [i].shadowColor = col;
//			shadows[i].SetPosition (actors [i].position);
//		}
//			
//
//	}
}
