using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    GameObject tl;      // タイトルロゴの影

    [SerializeField, Header("読み込むシーンの名前")]
    string[] SceneName = new string[] {"Main", "Config" };

    // 背景色
    Color[] SkyColor;

    // タイトル影、ポジション係数
    [SerializeField, Header("影のy移動")]
    float yTitleMove = 0.4f;

    void Start()
    {
        // カメラの背景色の設定
        SkyColor = new Color[] { IntToNorm(153, 255, 255), IntToNorm(204, 102, 0), IntToNorm(230, 230, 0) };

        // タイトルロゴが入れられてたら
        if(null != TitleLogo)
        {
            tl = Instantiate(TitleLogo) as GameObject;
            tl.GetComponent<Renderer>().material.color = IntToNorm(130, 130, 130);
            tl.GetComponent<Renderer>().sortingOrder = -1;
        }

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

        // 矢印の選択
        if (Input.GetKeyUp(KeyCode.Return))
        {
            if (t == 1) Application.Quit();
            else if (t == 0) SceneLoad(SceneName[0]);
            else SceneLoad(SceneName[1]);
        }

        // タイトルロゴの影の移動
        tl.transform.position = TitleLogo.transform.position + new Vector3(0.4f - (t * yTitleMove), -0.2f);

        // 文字の透過
        Rend(GameStart, (1 - t));
        Rend(GameStartJp, (1 - t));
        Rend(Config, 1 - Mathf.Abs(t - 0.5f));
        Rend(ConfigJp, 1 - Mathf.Abs(t - 0.5f));
        Rend(Quit, t);
        Rend(QuitJp, t);

        // カメラの背景の色の変更
        Camera.main.backgroundColor = (1 - t) * SkyColor[0] + t * SkyColor[1];

        // 実回転
        transform.rotation = Quaternion.AngleAxis((1-t) * minRotate + t * maxRotate, Vector3.forward);
	}

    // オブジェクトの透明度を変えます
   private void Rend(GameObject g, float alpha)
    {
        Color objColor = g.GetComponent<Renderer>().material.color;
        g.GetComponent<Renderer>().material.color = new Color(objColor.r, objColor.g, objColor.b, alpha);
    }

    // 255 -> 1 // 0 - 255 を 0 - 1 にします
    private Color IntToNorm(int r,int  g, int b)
    {
        Color rtnColor = new Color(r / 255.0f, g / 255.0f, b / 255.0f);
        return rtnColor;
    }

    private void SceneLoad(string name)
    {
        Debug.Log(name);
        //SceneManager.LoadScene(name);
    }
}

