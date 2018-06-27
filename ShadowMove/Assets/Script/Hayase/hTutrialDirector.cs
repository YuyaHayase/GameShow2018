using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class hTutrialDirector : MonoBehaviour {

    [SerializeField, Header("Player")]
    GameObject player;

    [SerializeField, Header("マップタイル")]
    GameObject[] Obj = new GameObject[4];

    [SerializeField, Header("ナビゲーションのテキスト")]
    string[] nav = new string[10];

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

    private void Start()
    {
        for (int i = 0; i < Obj.Length - 1; i++)
            if (Obj[i].tag != "Needle")
                Obj[i].tag = "Needle";

        if (null == player) GameObject.Find("player");
        memPos = player.transform.position.x;

        if (null != DisplayText)
            StartCoroutine("Disp");

        if (null == hackObj)
            Debug.Log("オブジェクトいれて");
    }

    // Update is called once per frame
    void Update () {
        if(Input.GetKeyDown(KeyCode.Q))
            hackObj.transform.position = hackObjFinalPos;

        if (!isPlay)
            cnt += 0.1f;

        if (cnt > 30 && isNext)
        {
            cnt = 0;
            isNext = false;
            txsel++;
            StartCoroutine("Disp");
        }

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
        if(nowPos - memPos > 0)
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

    private IEnumerator Disp()
    {
        for(int i = 0; i < nav[txsel].Length + 1; i++)
        {
            DisplayText.text = nav[txsel].Substring(0, i);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void ChangeSelect(int i)
    {
        Obj[i].tag = "block";
    }
}
