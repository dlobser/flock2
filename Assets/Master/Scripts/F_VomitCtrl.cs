using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class F_VomitCtrl : MonoBehaviour {

	ON.ON_ObjectPool pool;
	public float rate;
	float counter;
	public float lifetime;
	List<GameObject> vomits;
	List<float> vomitTime;
	GameObject vomitParent;

  public GameObject UpperBeak;
  public GameObject LowerBeak;
  public float beakSpeed;

  // Use this for initialization
  void Start () {
		vomitParent = GameObject.Find ("VomitParent");	
		pool = GameObject.Find ("VomitPool").GetComponent<ON.ON_ObjectPool>();
		vomits = new List<GameObject> ();
		vomitTime = new List<float> ();
	}

	public void DestroyVomit(GameObject vomit){
		int which = 0;
		vomit.GetComponent<TrailRenderer> ().time = 0;
		vomit.GetComponent<TrailRenderer> ().enabled = false;
		for (int i = 0; i < vomits.Count; i++) {
			if (vomits [i] == vomit) {
				pool.PoolDestroy (vomits [which]);
				vomits.RemoveAt (which);
				vomitTime.RemoveAt (which);
				i = vomits.Count;
				continue;

			}
			which++;
		}
	}
	
  IEnumerator AnimateBeak()
  {

    float counter = 0;
    while (counter < beakSpeed)
    {
           
      counter += Time.deltaTime;
      UpperBeak.transform.localEulerAngles = new Vector3(((Mathf.Cos((counter / beakSpeed)*6.28f) - 1f) * -.5f) * -20,0, 0);
      LowerBeak.transform.localEulerAngles = new Vector3(((Mathf.Cos((counter / beakSpeed)*6.28f) - 1f) * -.5f) * 20,0, 0);
      yield return null;
    }
  }
	void Update () {
		counter += Time.deltaTime * rate;
		if (counter > 1) {
      StartCoroutine(AnimateBeak());
			GameObject g = pool.PoolInstantiate ();
			Vector3 v = this.transform.position;
			g.transform.position = new Vector3 (v.x, v.y, v.z);
			g.transform.rotation = this.transform.rotation;
			g.transform.localScale = Vector3.zero;
			g.GetComponent<TrailRenderer> ().enabled = true;
			g.GetComponent<F_VomitMover> ().lifeTime = lifetime;
			g.GetComponent<F_VomitMover> ().counter = 0;
			g.GetComponent<F_VomitMover> ().vCtrl = this;
			g.transform.parent = vomitParent.transform;
			vomits.Add(g);
			vomitTime.Add (0);
			counter = 0;
		}
		for (int i = 0; i < vomits.Count; i++) {
			vomitTime [i] += Time.deltaTime;
			if (vomitTime [i] > lifetime) {
				vomitTime.RemoveAt (i);
				pool.PoolDestroy (vomits [i]);
				vomits.RemoveAt (i);
				break;
			}
		}
	}
}
