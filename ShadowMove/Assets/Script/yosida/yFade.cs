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
        transform.localRotation = Quaternion.Euler(180, 0, 0);
        fadeImage.fillMethod = Image.FillMethod.Radial360;
        fadeImage.fillAmount -= 0.01f;
        if (fadeImage.fillAmount <= 0)
            flgFadeIn = false;
    }

    private void FadeOut()
    {
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        fadeImage.fillMethod = Image.FillMethod.Radial360;
        fadeImage.fillAmount += 0.01f;

        if (fadeImage.fillAmount >= 1.0f)
        {
            //ステージをつくる
            _hMainDirector.EndedStage();

            SceneManager.LoadScene(nextScene);
            /*
            //プレイヤーと影の初期位置を最初の位置に
            player.transform.position = _yPlayerAI.StartPos;
            shodow.transform.position = new Vector3(-6, 4);

            //カメラの位置を影の位置に
            Camera.main.transform.position = shodow.transform.position + new Vector3(0, 0, -10);

            //プレイヤーの歩くスピードを元に戻す
            _yPlayerAI.Speed = _yPlayerAI.SpeedSave;

            //影の色を元の色に
            _fshodow.GetComponent<Renderer>().material.color = Color.black;

            //影のコライダーをfalseにする
            shodow.GetComponent<BoxCollider2D>().isTrigger = true;

            //背景の色を最初からに
            tim.mode = 1;
            tim.t = 0;

            //乗り移りやめ
            _fshodow.FlgPossess = false;

            //影を歩けるようにする
            _fshodow.isWalkFlg = true;

            //フェードアウト終わり
            flgFadeOut = false;

            //フェードインする
            flgFadeIn = true;
            */
        }
    }
}
