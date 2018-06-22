using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hTitleNeedle : MonoBehaviour {

    [SerializeField, Header("needle"), Range(0, 1)]
    float t = 0;

    // 回転の最小角度
    float minRotate = -45;
    float maxRotate = 45;

    // 加える値
    float add = 0.5f;

    // 値を変化する際の目標
    float next = 0;

    // 文字オブジェクトの変数
    [SerializeField, Header("文字")]
    GameObject GameStart;
    [SerializeField]
    GameObject GameStartJp;

    [SerializeField]
    GameObject Config;
    [SerializeField]
    GameObject ConfigJp;

    [SerializeField]
    GameObject Quit;
    [SerializeField]
    GameObject QuitJp;

    [SerializeField]
    GameObject TitleLogo;

    // 背景色
    Color[] SkyColor = {new Color(0.6f, 1, 1), new Color(0.8f,0.4f,0), new Color(0.9f,0.9f,0f) };

    // タイトル影、ポジション係数
    [SerializeField, Header("影のy移動")]
    float yTitleMove = 1.0f;

    void Start()
    {
        GameObject ts = GameObject.Find("TitleLogo") as GameObject;
        ts.GetComponent<Renderer>().material.color = Color.black;
        Instantiate(ts);
    }

    // Update is called once per frame
    void Update () {
        // 目標の変更
        if (Input.GetKeyDown(KeyCode.DownArrow)) next += add;
        if (Input.GetKeyDown(KeyCode.UpArrow)) next -= add;

        // 範囲を超えないようにする
        t = Mathf.Clamp(t, 0, 1f);
        next = Mathf.Clamp(next, 0, 1f);

        // アニメーション
        if (next != t) t += (next - t) * 0.1f;
        if (t < 0.025f) t = 0;
        if (t > 0.975f) t = 1;


        // 文字の透過
        Rend(GameStart, (1 - t));
        Rend(GameStartJp, (1 - t));
        Rend(Config, 1 - Mathf.Abs(t - 0.5f));
        Rend(ConfigJp, 1 - Mathf.Abs(t - 0.5f));
        Rend(Quit, t);
        Rend(QuitJp, t);

        Camera.main.backgroundColor = (1 - t) * SkyColor[0] + t * SkyColor[1];

        // 実回転
        transform.rotation = Quaternion.AngleAxis((1-t) * minRotate + t * maxRotate, Vector3.forward);
	}

   private void Rend(GameObject g, float alpha)
    {
        Color objColor = g.GetComponent<Renderer>().material.color;
        g.GetComponent<Renderer>().material.color = new Color(objColor.r, objColor.g, objColor.b, alpha);
    }
}

