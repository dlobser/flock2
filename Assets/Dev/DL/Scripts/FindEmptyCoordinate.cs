using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class FindEmptyCoordinate : MonoBehaviour {

	public Vector2 highBound;
	public Vector2 lowBound;
	public Vector2 divisions;
	public float newHeight;
	int amount;
	public GameObject obj;
	public GameObject actorParent;
	List<GameObject> boxes;
	public GameObject indicator;
	float X,Y;
	public Vector3 emptyCoordinate;
	public bool Rebuild = true;
	Coroutine finder;
//	Holojam.Tools.Holobounds holobounds;
	// Use this for initialization
	void Start () {
//		holobounds = GameObject.Find ("Holobounds").GetComponent<Holojam.Tools.Holobounds> ();

	
	}

	public void Build(){
		FindHoloBounds ();
		X = highBound.x - lowBound.x;
		Y = highBound.y - lowBound.y;
		amount = (int)( divisions.x * divisions.y);
		boxes = new List<GameObject> ();
		for (int i = 0; i < this.transform.childCount; i++) {
			if (this.transform.GetChild (0) != null)
				Destroy (this.transform.GetChild (0).gameObject);
		}
		for (int i = 0; i < divisions.x; i++) {
			for (int j = 0; j < divisions.y; j++) {
				GameObject g = Instantiate (obj);
				g.transform.parent = this.transform;
				g.transform.localScale =  new Vector3 (X/divisions.x, 50, Y/divisions.y);
				g.transform.localPosition = new Vector3 (
					(i * (X/divisions.x)) + lowBound.x + ((X/divisions.x)*.5f), 0, 
					(j * (Y/divisions.y)) + lowBound.y + ((X/divisions.x)*.5f));
				boxes.Add (g);
			}
		}
		if (finder != null)
			StopCoroutine (finder);
		finder = StartCoroutine (Finder ());
		Debug.Log (highBound + "," + lowBound + ",Holobounds");
	}

	void FindHoloBounds(){
//		highBound = new Vector2 (Mathf.Max (holobounds.bounds [1].x, holobounds.bounds [2].x)-1f,
//			Mathf.Max (holobounds.bounds [0].y, holobounds.bounds [1].y)-1f);
//		lowBound = new Vector2(Mathf.Min(holobounds.bounds [0].x,holobounds.bounds [3].x)+1f,
//			Mathf.Min(holobounds.bounds[3].y,holobounds.bounds[2].y)+1f);
	}

	void Update(){
		if (Rebuild) {
			Rebuild = false;
			Build ();
		}
	}


	public IEnumerator Finder(){

		while (true) {
			int allEmpty = 0;
			for (int i = 0; i < amount; i++) {
				int randI = (int)(Random.value * amount);
				bool empty = true;

				for (int j = 0; j < actorParent.transform.childCount; j++) {
					int randJ = j;
					if (checkBounds( actorParent.transform.GetChild (randJ).transform.position,boxes [randI]))	 {
						empty = false;
					
					}
				}

				if (empty) {
					Vector3 pos = boxes [randI].transform.position;
					Vector3 size = boxes [randI].transform.localScale * .5f;
					emptyCoordinate.Set (
						Random.Range (pos.x - size.x, pos.x + size.x), newHeight,
						Random.Range (pos.z - size.z, pos.z + size.z));
					indicator.transform.position = emptyCoordinate;
				} 
				else
					allEmpty++;
				yield return new WaitForSeconds (0);
			}
			if (allEmpty == amount) {
				int randI = (int)(Random.value * amount);
				Vector3 pos = boxes [randI].transform.position;
				Vector3 size = boxes [randI].transform.localScale * .5f;
				emptyCoordinate.Set (
					Random.Range (pos.x - size.x, pos.x + size.x), newHeight,
					Random.Range (pos.z - size.z, pos.z + size.z));
				indicator.transform.position = emptyCoordinate;

			}
		}
	}

	bool checkBounds(Vector3 check, GameObject box){
		Vector3 pos = box.transform.localPosition;
		Vector3 size = box.transform.localScale * .5f;
		if (check.x > pos.x - size.x && check.x < pos.x + size.x &&
		    check.z > pos.z - size.z && check.z < pos.z + size.z) {
			return true;
		} else
			return false;
	}

}
