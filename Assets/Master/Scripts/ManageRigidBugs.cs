using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageRigidBugs : MonoBehaviour {

	public GameObject Avatars;
	public GameObject RigidParent;
	public GameObject BugRigidPrefab;
	public GameObject Display;
	GameObject display;
	public float heightLerpSpeed;
	public int amount;

	Vector3 rigidToWorld;
	Vector3 worldWithNoise;

	public float noiseSpeed;
	public float noiseMult;
	public float noiseFreq;

	public float pushForce;
	public float pullForce;

	public ParticleSystem[] particleBursts;

	ImageTools.Core.PerlinNoise PNoise;

	public float initialPositionScale = 1;

	void Start () {
		if (Display != null) {
			display = new GameObject ();
			display.name = "display";
			for (int i = 0; i < amount; i++) {
				GameObject B = Instantiate (BugRigidPrefab,GetRandomPositionCircle(),Quaternion.identity,RigidParent.transform);
//				B.transform.SetParent (RigidParent.transform);

				GameObject b = Instantiate (Display);
				b.GetComponent<BugMVideo> ().id = i;
				b.transform.SetParent (display.transform);
			}
		}
		PNoise = new ImageTools.Core.PerlinNoise (1);
	}

	public void ResetRigidBody(int index, Vector3 Pos){
		RigidParent.transform.GetChild (index).transform.position = Pos;
	}
	
	void Update () {
		for (int i = 0; i < RigidParent.transform.childCount; i++) {
			for (int j = 0;j < Avatars.transform.childCount; j++) {
				Vector3 pos = RigidParent.transform.GetChild(i).position;
				Vector3 pos2 = Avatars.transform.GetChild(j).position;
				Vector2 Force = new Vector2(pos.x,pos.y) - new Vector2(pos2.x,pos2.z) ;
				Vector2 Force2 = new Vector2(pos2.x,pos2.z) - new Vector2(pos.x,pos.y)  ;
				float dist = Vector2.Distance(new Vector2(pos.x,pos.y) , new Vector2(pos2.x,pos2.z));
				Rigidbody2D B = RigidParent.transform.GetChild(i).gameObject.GetComponent<Rigidbody2D>();
				if(dist>1)
					B.AddForce ((Force2 / (dist))*pullForce);

				if(dist!=0)
				B.AddForce((Force/(dist*dist*dist))*pushForce);

				rigidToWorld = new Vector3 (B.transform.position.x, 
					Mathf.Lerp(display.transform.GetChild (i).position.y, Camera.main.transform.position.y, heightLerpSpeed), 
					B.transform.position.y);
				worldWithNoise = GetNoiseVec (rigidToWorld, noiseMult, noiseFreq, Time.time*noiseSpeed, 1);
				display.transform.GetChild (i).position = worldWithNoise;
			}
		}
	}


	public void ProcessCollision(BugMVideo b){
		ParticleSystem p = Instantiate (particleBursts[(int)(Random.value*particleBursts.Length)], b.transform.position, Quaternion.identity) as ParticleSystem;
		p.Emit (10);

	}

	public void ResetPosition(BugMVideo b){
		RigidParent.transform.GetChild (b.id).transform.position = GetRandomPositionCircle ();

	}

	Vector2 GetRandomPositionCircle(){
		Vector2 circle = Random.insideUnitCircle.normalized;
		return circle * initialPositionScale;;
	}

	public void LevelUp(BugMVideo b){

//		handler.EatBug();
//		score.GetComponent<keepScore>().AddToScore();

	}

	public void SwapTexture(BugMVideo b){
		b.transform.GetChild(0).gameObject.GetComponent<MaterialSwapperContinuous> ().swapMat ((int)(Random.value*6));
    SetMeshAttributes att = b.transform.GetChild(0).GetComponent<SetMeshAttributes>();
    float prev = att.whichBug;
    float curr = prev++;
    if (prev > 7)
      curr = 0;
    att.whichBug = curr;
  }


  Vector3 GetNoiseVec(Vector3 origin, float scale, float wScale,  float noiseCounter, float off){
		Vector3 newPos = origin;
		return origin + new Vector3 (
			scale * (float)PNoise.Noise (wScale * newPos.x + noiseCounter + off, noiseCounter + newPos.y * wScale, noiseCounter + wScale * newPos.z),
			scale * (float)PNoise.Noise (wScale * newPos.x + noiseCounter, wScale * newPos.y + noiseCounter + off, noiseCounter + wScale * newPos.z),
			scale * (float)PNoise.Noise (wScale * newPos.x + noiseCounter, wScale * newPos.y + noiseCounter, noiseCounter + wScale * newPos.z + off));
		
	}
}
