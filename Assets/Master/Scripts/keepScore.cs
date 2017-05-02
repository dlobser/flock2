using UnityEngine;
using System.Collections;

public class keepScore : MonoBehaviour {

    int score = -1;
    TextMesh text;
	// Use this for initialization
	void Start () {
        text = GetComponent<TextMesh>();
        AddToScore();
	}
	
    public void AddToScore()
    {
        score++;
        text.text = score + "\nBUGS";
    }
	// Update is called once per frame
	void Update () {
	
	}
}
