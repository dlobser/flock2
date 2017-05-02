using UnityEngine;
using System.Collections;


public class BugManager : MonoBehaviour {
//	public Bug bug;
	public Bug[] bugPrefabs;
	public int gridWidth = 2;
	public float scale = 0.5f;

	public float disableTime = 1;

	public bool init = false; //redundant?

	int amount;

	public Bug[] bugs;
	Transform[] nearestActors;
	Vector3[] nearestBugOriginOffset;
	Vector3[] nearestBugLerp;
	Vector3[] targets;
	Transform[] actors;

	Vector3[] noiseVecs;

	float noiseCounter = 0;
	public float noiseSpeed = 1f;
	public float noiseScale = 2.2f;
	public float noiseWorldScale = .1f;

	public float bugHeight = 0;

	ImageTools.Core.PerlinNoise PNoise;
	public ParticleSystem[] part;

	public bool rebuild = false;

	GameObject spawnPoint;

	FaderManager fader;
	LevelHandler handler;

	FindEmptyCoordinate emptyCoordinate;

	void Awake(){
		Build ();
		emptyCoordinate = GameObject.Find ("GridCheck").gameObject.GetComponent<FindEmptyCoordinate> ();
		fader =  GameObject.Find ("LevelFader").gameObject.GetComponent<FaderManager>();
		handler =  GameObject.Find ("LevelHandler").gameObject.GetComponent<LevelHandler>();
	}

	void Build(){
		if(!Application.isPlaying)return;

		if (rebuild) {
			for (int i = 0; i < amount; i++) {
				Destroy (bugs [i].gameObject);
			}
		}

		if (spawnPoint == null) {
			spawnPoint = GameObject.Find ("BugSpawnPoint");
		}

		amount = gridWidth * gridWidth;
		bugs = new Bug[amount];
		nearestActors = new Transform[amount];
		targets = new Vector3[amount];
		nearestBugOriginOffset = new Vector3[amount];
		nearestBugLerp = new Vector3[amount];
		noiseVecs = new Vector3[amount];

		Transform actorManager = GameObject.Find ("ActorManager").transform;
		actors = new Transform[actorManager.childCount];

		for (int i = 0; i < actors.Length; i++) {
			actors [i] = actorManager.GetChild (i);
		}


		PNoise = new ImageTools.Core.PerlinNoise (1);


		for(int x = 0 ; x < gridWidth ; ++x ){
			for (int y = 0 ; y < gridWidth ; ++y) {
				Vector3 posA = new Vector3 ((x-gridWidth/2) * scale, bugHeight, (y-gridWidth/2) * scale);
				Vector3 pos = transform.position + posA;
				targets [x + gridWidth * y] = pos;
				GameObject b = bugPrefabs [(int)Mathf.Floor (Random.value * bugPrefabs.Length)].gameObject;
				GameObject myBug = Instantiate (b.gameObject, pos, Quaternion.identity) as GameObject;
				myBug.transform.parent = transform;
				string label = "Bug" + x + "." + y;
				myBug.name = label;
				bugs [x + gridWidth * y] = myBug.GetComponent<Bug> ();
//				bugs [x + gridWidth * y].label = label;
				bugs [x + gridWidth * y].origin = pos;
				nearestBugLerp [x + gridWidth * y] = pos;
			}
		}

		StartCoroutine (FindNearestBugs ());
		init = true;

	}

	void Update(){
		if(init)
			UpdateBugPosition ();
		if (rebuild) {
			Build ();
			rebuild = false;
		}
	}



	void UpdateBugPosition(){
		for (int i = 0; i < amount; i++) {
			float off = .5f;
			float scale = noiseScale*.01f;
			float wScale = noiseWorldScale;
			noiseCounter += noiseSpeed * Time.deltaTime;
			if(nearestActors[i]!=null)
				nearestBugLerp[i] = Vector3.Lerp (nearestBugLerp[i], nearestActors [i].transform.position, .05f);
			nearestBugOriginOffset [i].Set (bugs [i].origin.x, nearestBugLerp [i].y, bugs [i].origin.z);
			Vector3 newPos = bugs [i].origin;
			newPos = nearestBugOriginOffset[i] + new Vector3 (
				scale * (float)PNoise.Noise (wScale * newPos.x + noiseCounter + off, noiseCounter + newPos.y * wScale, noiseCounter + wScale * newPos.z),
				scale * (float)PNoise.Noise (wScale * newPos.x + noiseCounter, wScale * newPos.y + noiseCounter + off, noiseCounter + wScale * newPos.z),
				scale * (float)PNoise.Noise (wScale * newPos.x + noiseCounter, wScale * newPos.y + noiseCounter, noiseCounter + wScale * newPos.z + off));
			bugs [i].transform.position = newPos;
			bugs [i].transform.GetChild (0).GetComponent<SetMeshAttributes> ().Set ();
		} 

	}

	IEnumerator FindNearestBugs(){
		while (true) { 
			for (int i = 0; i < amount; i++) {
				nearestActors [i] = DLUtility.GetClosestGameObject (bugs [i].transform, actors);
				yield return new WaitForSeconds (0);
			}
		}
	}

	void UpdateBugAppearance(Bug b){
//		b.renderers[0].material=
		Debug.Log (b);
	}


	public void ProcessCollision(Bug b){
		if (!init)
			return;
		else {
			ParticleSystem p = Instantiate (part[(int)(Random.value*part.Length)], b.transform.position, Quaternion.identity) as ParticleSystem;
			p.Emit (10);

		}
	}

	public void LevelUp(Bug b){
		if (!init)
			return;
//		else
//			handler.EatBug ();
//			fader.level += 1;
	}


	public void SwapTexture(Bug b){
		if (!init)
			return;
		else {
			b.transform.GetChild(0).gameObject.GetComponent<MaterialSwapperContinuous> ().swapMat ();

		}
	}

	public void ResetPosition(Bug b){
		if (!init)
			return;
		else {
//			Vector3 rand = Random.insideUnitSphere;
			b.origin = emptyCoordinate.emptyCoordinate;// spawnPoint.transform.position + new Vector3(rand.x,b.transform.position.y,rand.z) * scale * (gridWidth * .5f);

		}
	}
}