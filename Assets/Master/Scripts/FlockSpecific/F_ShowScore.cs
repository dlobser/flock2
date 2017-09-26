using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_ShowScore : MonoBehaviour {

    public TextMesh tMesh;
    public LevelHandler levelH;
	// Use this for initialization
	void OnEnable () {
        tMesh.text = "You Ate " + levelH.bugsEaten + " Bugs";
        StartCoroutine(FadeUp());
	}

    IEnumerator FadeUp()
    {
        float counter = 0;
        while (counter < 1)
        {

            counter += Time.deltaTime;
            tMesh.color = new Color(.8f, 1, .9f, counter);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return null;
    }
	
}
