using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class yEndingFade : MonoBehaviour {

    Image fadeImage;

    RectTransform fadeRect;

    [SerializeField,Header("次のシーン")]
    string nextScene;

    [SerializeField,Header("Fadeするときの画像のマイナス値")]
    Vector2 decSize = new Vector2(10,10);

    bool flgFadeOut = false;

    public bool FlgFadeOut
    {
        get { return flgFadeOut; }
        set { flgFadeOut = value; }
    }

	// Use this for initialization
	void Start () {
        fadeImage = GetComponent<Image>();
        fadeRect = GetComponent<RectTransform>();

    }
	
	// Update is called once per frame
	void Update () {

        if (flgFadeOut)
            FadeOut();
	}

    private void FadeOut()
    {
        fadeRect.sizeDelta -= decSize;
        fadeImage.transform.localPosition = new Vector3(0, -100.0f, 0);


        if(fadeRect.sizeDelta.x <= 800.0f)
        {
            fadeRect.sizeDelta = new Vector2(800.0f, 800.0f);
            SceneManager.LoadScene(nextScene);
        }
    }
}
//800 800
