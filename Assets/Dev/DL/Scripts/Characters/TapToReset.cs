using UnityEngine;
using System.Collections;

public class TapToReset : MonoBehaviour {

	LevelHandler lHandler;
	public GameObject[] activateOnReset;
	public GameObject[] deactivateOnReset;
	public Resetable[] reset;
	public string[] resetPosition;

	void Start () {
		lHandler = GameObject.Find ("LevelHandler").GetComponent<LevelHandler> ();
	}

	public void Reset(){
		for (int i = 0; i < activateOnReset.Length; i++) {
			if (!activateOnReset [i].activeInHierarchy)
				activateOnReset [i].gameObject.SetActive (true);
		}
		for (int i = 0; i < deactivateOnReset.Length; i++) {
			if (deactivateOnReset [i].activeInHierarchy)
				deactivateOnReset [i].gameObject.SetActive (false);
		}
		for (int i = 0; i < reset.Length; i++) {
			reset [i].Reset ();
		}
        for (int i = 0; i < resetPosition.Length; i++)
        {
            if (GameObject.Find(resetPosition[i])!=null)
                GameObject.Find(resetPosition[i]).transform.position = Vector3.zero;
        }
		lHandler.Reset ();
	}

    
//    }
}
