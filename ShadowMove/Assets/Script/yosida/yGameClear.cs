using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
class StaffRoll
{
    [SerializeField,Header("テキスト")]
    string text;
    [SerializeField,Header("位置")]
    Vector3 textPos;
    [SerializeField, Header("高さ　幅")]
    Vector2 size;

    public string Text
    {
        set { text = value; }
        get { return text; }
    }

    public Vector3 TextPos
    {
        set { textPos = value; }
        get { return textPos; }
    }

    public Vector2 Size
    {
        get { return size; }
        set { size = value; }
    }
}

public class yGameClear : MonoBehaviour {

    [SerializeField,Header("テキスト")]
    Text textPrefab;
    [SerializeField,Header("テキストの影")]
    Text textShodowPrefab;

    [SerializeField, Header("スタッフロール")]
    List<StaffRoll> staffRoll;

    RectTransform[] textRect;
    RectTransform[] textShodowRect;

    Vector2[] textSize;
    Vector2 maxSize = new Vector2(341.1f, 78.8f);

    Text[] text;
    Text[] textShodow;

    [SerializeField,Header("どれだけ出したいか")]
    int[] displayNum;
    int num = 0;    //表示した数　添え字
    int disNum = 0; //desplayNumの添え字

    float alpha = 1.0f;
    [SerializeField,Header("")]
    float t;
    [SerializeField]
    float accele = 0.5f;
    [SerializeField, Header("")]
    float alphaAccele = 0.5f;

    yEndingFade _yEndingFade;
    yPlayerAI _yPlayerAI;

	// Use this for initialization
	void Start () {

        _yEndingFade = GameObject.Find("Fade").GetComponent<yEndingFade>();
        _yPlayerAI = GameObject.Find("player").GetComponent<yPlayerAI>();

        int n = staffRoll.Count;

        //配列の要素数
        text = new Text[n];
        textShodow = new Text[n];
        textSize = new Vector2[n];
        textRect = new RectTransform[n];
        textShodowRect = new RectTransform[n];

        Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        for (int i = 0; i < text.Length; i++)
        {
            //オブジェクトの生成
            textShodow[i] = Instantiate(textShodowPrefab) as Text;
            text[i] = Instantiate(textPrefab) as Text;

            //Canvasの子オブジェクト
            textShodow[i].transform.parent = canvas.transform;
            text[i].transform.parent = canvas.transform;

            //生成したもののサイズ
            text[i].transform.localScale = Vector3.one;
            textShodow[i].transform.localScale = Vector3.one;

            //高さ　幅
            text[i].GetComponent<RectTransform>().sizeDelta = staffRoll[i].Size;
            textShodow[i].GetComponent<RectTransform>().sizeDelta = staffRoll[i].Size;

            //RectTransformを取得
            textRect[i] = text[i].GetComponent<RectTransform>();
            textShodowRect[i] = textShodow[i].GetComponent<RectTransform>();

            //テキストと位置を設定
            text[i].text = staffRoll[i].Text;
            text[i].transform.localPosition = staffRoll[i].TextPos;

            //テキストの影の位置
            textShodow[i].text = text[i].text;
            textShodow[i].transform.localPosition = text[i].transform.localPosition + new Vector3(13.7f, -14.4f, 0);

            //サイズ
            textSize[i] = textRect[i].sizeDelta;

            textRect[i].sizeDelta = new Vector2(0, textSize[i].y);
            textShodowRect[i].sizeDelta = new Vector2(0, textSize[i].y);

            text[i].enabled = false;
            textShodow[i].enabled = false;
        }

        textPrefab.enabled = false;
        textShodowPrefab.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        
        //線形補完の割合を増やす
        t += Time.deltaTime * accele;


        if (disNum < displayNum.Length)
        {
            for (int i = num; i < displayNum[disNum] + num; i++)
            {
                text[i].enabled = true;
                textShodow[i].enabled = true;

                //線形補完
                if (t < 1)
                {
                    textRect[i].sizeDelta = LinearInterpoltion(textRect[i].sizeDelta, maxSize);
                    textShodowRect[i].sizeDelta = LinearInterpoltion(textRect[i].sizeDelta, maxSize);
                }

                //線形補完が終わったら消えていく
                if (t >= 1)
                {
                    alpha -= Time.deltaTime;
                    text[i].color = new Color(text[i].color.r, text[i].color.g, text[i].color.b, alpha);
                    textShodow[i].color = new Color(textShodow[i].color.r, textShodow[i].color.g, textShodow[i].color.b, alpha);

                    if (alpha < 0.0f)
                    {
                        alpha = 1.0f;
                        t = 0;
                        num += displayNum[disNum];
                        disNum++;
                        break;
                    }
                }
            }
        }
        else
        {
            //フェードアウトをする
            _yEndingFade.FlgFadeOut = true;

            //Playerが歩くのをやめる
            _yPlayerAI.Work = 0.0f;
        }
    }

    //横幅の長さを広くする線形補完
    private Vector2 LinearInterpoltion(Vector2 myWidth, Vector2 changeWidth)
    {
        float width = (1.0f - t) * myWidth.x + t * changeWidth.x;

        return new Vector2(width, changeWidth.y);
    }


}
