using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class FaderText : Fader {

	public string[] text;
	public TextMesh display;

    MeshRenderer mRend;
    Material mat;

    public float fadeSpeed = 1;

    Color initColor;

    int prevLevel;

    private void Start() {
        mRend = display.gameObject.GetComponent<MeshRenderer>();
        mat = mRend.material;
        initColor = mat.color;
    }

    public override void Init() {
        levels = max - min;
        Fade();
        display.text = text[0];
    }


    public override void Fade(){
		
		float currentLevel = ((Mathf.Clamp (level, min, max)-min) / levels) * text.Length;
        int intLevel = (int)Mathf.Clamp(Mathf.Floor(currentLevel), 0, text.Length - 1);

        string thisText = text [intLevel];

        if(prevLevel != intLevel)
            StartCoroutine(FadeText(thisText));

        if(mRend!=null)
            if (!mRend.enabled)
			    mRend.enabled = true;

        prevLevel = intLevel;
	}

    IEnumerator FadeText(string text) {
        float counter = 0;
        while (counter < fadeSpeed) {
            counter += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
            mat.color = new Color(initColor.r, initColor.g, initColor.b, 1 - (counter / fadeSpeed));
        }
        //Debug.Log("text: " + text);
        display.text = text;
        counter = 0;
        while (counter < fadeSpeed) {
            counter += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
            //Debug.Log(counter);
            mat.color = new Color(initColor.r, initColor.g, initColor.b, (counter / fadeSpeed));
        }
        yield return null;
    }
}
