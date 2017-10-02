using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_VomitMover : MonoBehaviour {

	public float speed;
	public float maxScale;
	public float maxTrailTime;
	public float lifeTime;
	public float counter;
	Vector3 rando;
	TrailRenderer tRend;
	public F_VomitCtrl vCtrl { get; set; }
	public float bendAmount = 1;
	public GameObject bugBurst;

	public string avatarName = "BirdAvatar";

	void OnTriggerEnter(Collider c){
		if(c.name == "Viewer" || c.name==avatarName){
			GameObject b = Instantiate (bugBurst);
			b.transform.position = this.transform.position;
			b.GetComponent<ParticleSystem> ().Emit (40);
			vCtrl.SendMessage("DestroyVomit", this.gameObject); //Callback
		}
		//Debug.Log (c.name);
	}

	void Start () {
		tRend = GetComponent<TrailRenderer> ();
		rando = Random.insideUnitSphere;
	}
		
	
	void Update () {
		counter += Time.deltaTime;
		this.transform.Translate (Vector3.forward * speed);
		this.transform.Rotate (rando * bendAmount);
		this.transform.localScale = maxScale * cosScale ((counter / lifeTime)*6.28f) * Vector3.one;
		tRend.time = maxTrailTime * cosScale ((counter / lifeTime) * 6.28f);
	}

	float cosScale(float t){
		return (Mathf.Cos(t)-1)*-.5f;
	}
}
