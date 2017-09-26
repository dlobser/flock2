using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class F_DefaultIP : MonoBehaviour {
  public string name = "IP";
  InputField txt;
	// Use this for initialization
	void Start () {
    txt = GetComponent<InputField>();
    //TextAsset t = Resources.Load(name) as TextAsset;
    txt.text =  System.IO.File.ReadAllText(Application.dataPath + "/IP.txt");
    Debug.Log(Application.dataPath);
  }

}
