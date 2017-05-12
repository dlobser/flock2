using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject serverCanvas, clientCanvas;
    public ClientHUD clientHudScript;
    public ServerHUD serverHUDScript;

	public GameObject[] show;
	public GameObject[] hide;

    public void StartServer()
    {
        serverHUDScript.enabled = true;
        serverCanvas.SetActive(true);    
		for (int i = 0; i < hide.Length; i++) {
			hide [i].SetActive (false);
		}
//        SceneManager.LoadScene("ServerClientMenu");
    }

    public void StartClient()
    {
        clientHudScript.enabled = true;
        clientCanvas.SetActive(true);
		for (int i = 0; i < hide.Length; i++) {
			hide [i].SetActive (false);
		}
//        SceneManager.LoadScene("ServerClientMenu");
    }
}
