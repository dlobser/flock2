using UnityEngine;
using UnityEngine.SceneManagement;

using System.Collections;

public class SceneReloader : MonoBehaviour
{
	public float timeBetweenSceneReloads = 120;
	float lastSceneLoadTime;
	public bool reloadNow;
	public float timeTillNextReload;

	// Use this for initialization
	void Start ()
	{
		reloadNow = false; 
		lastSceneLoadTime = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
	{
		timeTillNextReload = timeBetweenSceneReloads - ( Time.time - lastSceneLoadTime );

		if (reloadNow) { 
			LoadInScene ();

		}			
		//TODO: Move to SettingsManager
		if (Time.time - lastSceneLoadTime > timeBetweenSceneReloads) {
			LoadInScene ();

		}
	}

	void LoadInScene ()
	{ 
		Debug.LogWarning ("Loading Scene");
		SceneManager.LoadScene (1, LoadSceneMode.Single);
		lastSceneLoadTime = Time.time;
		reloadNow = false;
	}
}

