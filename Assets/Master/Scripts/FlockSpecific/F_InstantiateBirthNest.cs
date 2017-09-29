using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_InstantiateBirthNest : MonoBehaviour {

	void OnEnable () {
        if (GameObject.FindObjectOfType<F_IsLocalPlayer>() != null) {
            GameObject avatar = GameObject.FindObjectOfType<F_IsLocalPlayer>().gameObject;
            this.transform.position = new Vector3(avatar.transform.position.x, 0, avatar.transform.position.z);
            this.transform.eulerAngles = new Vector3(0, avatar.transform.eulerAngles.y, 0);
        }
        else this.gameObject.SetActive(false);
	}

}
