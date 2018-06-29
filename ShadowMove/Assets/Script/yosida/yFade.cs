using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class yFade : MonoBehaviour {

    GameObject player;
    GameObject shodow;
    Image fadeImage;

    string nextScene = "Main";

    bool flgFadeIn = true;
    bool flgFadeOut = false;
    hMainDirector _hMainDirector;
    fShodow _fshodow;
    yPlayerAI _yPlayerAI;

    timeover tim;

    public string NextScene
    {
        set { nextScene = value; }
    }

    public bool FlgFadeOut
    {
        set { flgFadeOut = value; }
    }

	// Use this for initialization
	void Start () {
        tim = Camera.main.GetComponent<timeover>();
        player = GameObject.Find("player");
        shodow = GameObject.Find("shodow");
        _hMainDirector = GameObject.Find("MainDirector").GetComponent<hMainDirector>();
        fadeImage = GetComponent<Image>();
        fadeImage.fillAmount = 1.0f;
        _fshodow = shodow.GetComponent<fShodow>();
        _yPlayerAI = player.GetComponent<yPlayerAI>();

    }
	
	// Update is called once per frame
	void Update () {
        if (flgFadeIn)
            FadeIn();

        if (flgFadeOut)
            FadeOut();
	}

    private void FadeIn()
    {
        fadeImage.fillMethod = Image.FillMethod.Radial360;
        fadeImage.fillAmount -= 0.01f;
        if (fadeImage.fillAmount <= 0)
            flgFadeIn = false;
    }

    private void FadeOut()
    {
        fadeImage.fillMethod = Image.FillMethod.Radial360;
        fadeImage.fillAmount += 0.01f;

        if (fadeImage.fillAmount >= 1.0f)
        {
            _hMainDirector.EndedStage();

            flgFadeOut = false;
            _fshodow.FlgPossess = false;

            player.transform.position = _yPlayerAI.StartPos;
            shodow.transform.position = new Vector3(-6, 4);

            Camera.main.transform.position = shodow.transform.position + new Vector3(0, 0, -10);

            _yPlayerAI.Speed = _yPlayerAI.SpeedSave;

            flgFadeIn = true;
            
            _fshodow.isWalkFlg = true;
            _fshodow.GetComponent<Renderer>().material.color = Color.black;
            shodow.GetComponent<BoxCollider2D>().isTrigger = false;
            tim.mode = 1;
            tim.t = 0;
        }
    }
}
