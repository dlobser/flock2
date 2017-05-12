using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour {


	public GameObject serverCanvas, clientCanvas;
	public ClientHUD clientHudScript;
	public ServerHUD serverHUDScript;

	public GameObject[] show;
	public GameObject[] hide;

	public void PressButton()
	{
		serverHUDScript.enabled = false;
		clientHudScript.enabled = false;
		serverCanvas.SetActive(false);    
		clientCanvas.SetActive (false);
		for (int i = 0; i < hide.Length; i++) {
			hide [i].SetActive (false);
		}
		for (int i = 0; i < show.Length; i++) {
			show [i].SetActive (true);
		}
		//        SceneManager.LoadScene("ServerClientMenu");
	}

}
