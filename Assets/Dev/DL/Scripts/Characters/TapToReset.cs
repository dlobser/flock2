using UnityEngine;
using System.Collections;

public class TapToReset : MonoBehaviour {
	
//	float tapCounter = 1.5f;
//	int taps;

	LevelHandler lHandler;
	public GameObject[] activateOnReset;
	public GameObject[] deactivateOnReset;
  public string[] resetPosition;

	// Use this for initialization
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
        for (int i = 0; i < resetPosition.Length; i++)
        {
            if (GameObject.Find(resetPosition[i])!=null)
                GameObject.Find(resetPosition[i]).transform.position = Vector3.zero;
        }
		lHandler.Reset ();
	}

    // Update is called once per frame
    void Update()
    {
        //tapCounter -= Time.deltaTime;
        //if (tapCounter <= 0)
        //{
        //    tapCounter = 1.5f;
        //    if (taps > 0)
        //        taps--;
        //}
        if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.LeftAlt))// && tapCounter > 0)
        {
            Debug.Log("reset");
            //taps++;
            //tapCounter = 1.5f;
            //if (taps == 4)
            //{
            //    //				Debug.Log ("poop");
                Reset();
            //    taps = 0;
            //}
        }
    }
}
