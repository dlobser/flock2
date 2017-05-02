using UnityEngine;
using System.Collections;

public class GetSpeed : MonoBehaviour {

	public float speed;
	Vector3 previous;
	public float instantaneousSpeed;
	public float averageSpeedOverThirty;
	public float averageSpeedOverThousand;
	public GameObject ObjectToTrack;
	// Use this for initialization
	void Start () {
		if (ObjectToTrack == null) {
			ObjectToTrack = this.gameObject;
		}
		StartCoroutine (updateSpeed ());
	}
	
	// Update is called once per frame
	IEnumerator updateSpeed () {
		while (true) {
			instantaneousSpeed = Vector3.Distance (previous, ObjectToTrack.transform.position);
			averageSpeedOverThirty += instantaneousSpeed;
			averageSpeedOverThirty /= 31f;
			averageSpeedOverThousand += (averageSpeedOverThousand*100f)+instantaneousSpeed;
			averageSpeedOverThousand /= 103f;
			previous = ObjectToTrack.transform.position;
//			Debug.Log (averageSpeedOverThousand);
			yield return new WaitForSeconds (Time.deltaTime);
		}
	}
}
