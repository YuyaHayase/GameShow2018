using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// チュートリアル
public class hTutrialDirector : MonoBehaviour {

    // プレイヤーオブジェクト
    [SerializeField, Header("Player")]
    GameObject player;

    [SerializeField, Header("マップタイル")]
    GameObject[] Obj = new GameObject[4];

    [SerializeField, Header("ナビゲーションのテキスト")]
    string[] nav = new string[10];

    // 画面の上でナビし続けるテキスト
    [SerializeField]
    Text DisplayText;

    [SerializeField, Header("乗り移るオブジェクト")]
    GameObject hackObj;
    Vector3 hackObjFinalPos = new Vector3(106, -3);


    [SerializeField, Header("プレイヤーの移動速度")]
    float PlayerSpeed = 0.05f;

    // マップパネル選択
    int sel = 0;

    // テキスト選択
    int txsel = 0;

    // プレイヤーのポジション保存
    float memPos = 0;

    // 次へ行っていいか
    [SerializeField]
    bool isNext = true;

    // フレームカウント
    [SerializeField]
    float cnt = 0;

    // プレイヤーが操作するか
    bool isPlay = false;

    // チュートリアルを始めるか
    bool TutStart = false;

    // なんフレーム経ったら始めるか
    [SerializeField, Header("なんフレーム経ったら始めるか")]
    float Tut_cnt = 60;

    private void Start()
    {
        // チュートリアル専用ステージのマップタイルのタグを変更
        for (int i = 0; i < Obj.Length - 1; i++)
            if (Obj[i].tag != "Needle")
                Obj[i].tag = "Needle";

        if (null == player) GameObject.Find("player");
        memPos = player.transform.position.x;

        // １文字ずつ表示させる
        if (null != DisplayText)
            StartCoroutine("Disp");

        if (null == hackObj)
            Debug.Log("オブジェクトいれて");
    }

    // Update is called once per frame
    void Update () {
        // ギミックのクリア
        if( Input.GetKey(KeyCode.LeftShift) & Input.GetKeyDown(KeyCode.Q))
            hackObj.transform.position = hackObjFinalPos;

        // 操作
        if (!isPlay)
            cnt += 0.1f;

        // チュートリアルが始まるまで少し待つので、待つ
        if (cnt > 60 && !TutStart)
            TutStart = true;
        if (TutStart)
        {
            // 35フレーム経ったら次のテキスト
            if (cnt > 35 && isNext)
            {
                cnt = 0;
                isNext = false;
                txsel++;
                StartCoroutine("Disp");
            }

            // テキストの選択によって、プレイヤーが行動するか変わる
            switch (txsel)
            {
                case 1:
                    ChangeSelect(0);
                    break;
                case 3:
                    ChangeSelect(1);
                    break;
                case 6:
                    isPlay = true;
                    if ((hackObj.transform.position - hackObjFinalPos).magnitude < 1)
                    {
                        hackObj.transform.position = hackObjFinalPos;
                        isPlay = false;
                        ChangeSelect(2);
                    }
                    break;
                case 10:
                    SceneManager.LoadScene("Main");
                    break;

            }

            float nowPos = player.transform.position.x;
            if (nowPos - memPos > 0)
            {
                // yPlayerAIで移動をかけているのでゆっくりにするため
                player.transform.Translate(new Vector3(-PlayerSpeed, 0, 0));
            }
            else
            {
                isNext = true;
            }
            memPos = nowPos;
        }
    }

    // コルーチン -> テキストを一文字ずつ表示させます
    private IEnumerator Disp()
    {
        for(int i = 0; i < nav[txsel].Length + 1; i++)
        {
            DisplayText.text = nav[txsel].Substring(0, i);
            yield return new WaitForSeconds(0.1f);
        }
    }

    // オブジェクトのタグを Needle から block にします
    private void ChangeSelect(int i)
    {
        Obj[i].tag = "block";
    }
}
