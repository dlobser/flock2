using UnityEngine;
using System.Collections;


public class BugManagerMVideo : MonoBehaviour {
	//	public Bug bug;
	public BugMVideo[] bugPrefabs;
	public int amount = 2;
	public float scale = 0.5f;

	public float bugLerpToOriginSpeed = .01f;
	public float disableTime = 1;

	public bool init = false; //redundant?

	public BugMVideo[] bugs;
	//	Transform[] nearestActors;
	Vector3[] nearestBugOriginOffset;
	Vector3[] nearestBugLerp;
	Vector3[] targets;
	//	Transform[] actors;

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

	public FaderManager fader;
	LevelHandler handler;

	FindEmptyCoordinateMVideo emptyCoordinate;

    bool upDown = true;

    GameObject score;

	GameObject[] Viewers;

	void Awake(){
		Build ();
		Viewers = GameObject.FindGameObjectsWithTag ("Viewer");
        score = GameObject.Find("score").gameObject;
		emptyCoordinate = GameObject.Find ("FindEmptyCoordinates_ForBugs").gameObject.GetComponent<FindEmptyCoordinateMVideo> ();
		//fader =  GameObject.Find ("LevelFader").gameObject.GetComponent<FaderManager>();
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

		bugs = new BugMVideo[amount];
		targets = new Vector3[amount];
		nearestBugOriginOffset = new Vector3[amount];
		nearestBugLerp = new Vector3[amount];
		noiseVecs = new Vector3[amount];

		PNoise = new ImageTools.Core.PerlinNoise (1);


		for(int x = 0 ; x < amount ; ++x ){
			Vector3 posA = new Vector3 (0, 0, 0);
			Vector2 posB = Random.insideUnitCircle.normalized;
			posA = new Vector3 (posB.x, 0, posB.y) * 10;
			Vector3 pos = transform.position + posA;
			targets [x] = pos;
			GameObject b = bugPrefabs [(int)Mathf.Floor (Random.value * bugPrefabs.Length)].gameObject;
			GameObject myBug = Instantiate (b.gameObject, pos, Quaternion.identity) as GameObject;
            myBug.transform.GetChild(0).gameObject.GetComponent<MaterialSwapperContinuous>().swapMat((int)(Random.value * 6));
            myBug.transform.parent = transform;
			string label = "Bug" + x;
			myBug.name = label;
			bugs [x ] = myBug.GetComponent<BugMVideo> ();
			bugs [x ].label = label;
//			bugs [x ].origin = pos;
			nearestBugLerp [x] = pos;
		}

		//		StartCoroutine (FindNearestBugs ());
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

			//move bugs toward the camera

//			float dist = Vector3.Distance (bugs [i].origin, Camera.main.transform.position);
////			if ( > 2) {
////				bugs [i].origin = Vector3.Lerp (bugs [i].origin, Camera.main.transform.position, bugLerpToOriginSpeed);
////			}
//
//			//			if(nearestActors[i]!=null)
//			bugs [i].originLerp = Vector3.Lerp( bugs [i].originLerp, Camera.main.transform.position, .05f);
//			bugs [i].originOffset.Set (bugs [i].origin.x, bugs [i].originLerp.y, bugs [i].origin.z);
//            Vector3 newPos = bugs [i].origin;
////			Debug.Log (newPos);

//			newPos = bugs [i].origin + new Vector3 (
//				scale * (float)PNoise.Noise (wScale * newPos.x + noiseCounter + off, noiseCounter + newPos.y * wScale, noiseCounter + wScale * newPos.z),
//				scale * (float)PNoise.Noise (wScale * newPos.x + noiseCounter, wScale * newPos.y + noiseCounter + off, noiseCounter + wScale * newPos.z),
//				scale * (float)PNoise.Noise (wScale * newPos.x + noiseCounter, wScale * newPos.y + noiseCounter, noiseCounter + wScale * newPos.z + off));
//			bugs [i].transform.position = newPos;
//			bugs [i].transform.GetChild (0).GetComponent<SetMeshAttributes> ().Set ();
		} 

	}

	//	IEnumerator FindNearestBugs(){
	//		while (true) { 
	//			for (int i = 0; i < amount; i++) {
	//				nearestActors [i] = DLUtility.GetClosestGameObject (bugs [i].transform, actors);
	//				yield return new WaitForSeconds (0);
	//			}
	//		}
	//	}

	void UpdateBugAppearance(Bug b){
		//		b.renderers[0].material=
		Debug.Log (b);
	}


	public void ProcessCollision(BugMVideo b){
		if (!init)
			return;
		else {
			ParticleSystem p = Instantiate (part[(int)(Random.value*part.Length)], b.transform.position, Quaternion.identity) as ParticleSystem;
			p.Emit (10);

		}
	}

	public void LevelUp(BugMVideo b){
        if (!init)
            return;
        else
        {
            handler.EatBug();
            score.GetComponent<keepScore>().AddToScore();
        }
            //changeLevel();
			//fader.level += 1;
	}

    public void changeLevel()
    {
        if(upDown && fader.level < fader.maxLevel)
        {
            fader.level++;
        }
        if (!upDown && fader.level > 0)
        {
            fader.level++;
        }
        if (fader.level >= fader.maxLevel || fader.level<=0)
        {
            upDown = false;
        }
    }
	public void SwapTexture(BugMVideo b){
		if (!init)
			return;
		else {
			b.transform.GetChild(0).gameObject.GetComponent<MaterialSwapperContinuous> ().swapMat ((int)(Random.value*6));

		}
	}

	public void ResetPosition(BugMVideo b){
		if (!init)
			return;
		else {
			//			Vector3 rand = Random.insideUnitSphere;
			Vector3 newOrigin = emptyCoordinate.GetEmptyCoordinate ();
//			b.origin = newOrigin;//emptyCoordinate.GetEmptyCoordinate() ;// spawnPoint.transform.position + new Vector3(rand.x,b.transform.position.y,rand.z) * scale * (gridWidth * .5f);
//			b.originLerp = newOrigin;
//			b.originOffset = newOrigin;
		}
	}
}