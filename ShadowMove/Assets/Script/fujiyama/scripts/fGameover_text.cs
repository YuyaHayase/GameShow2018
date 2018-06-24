using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class fGameover_text : MonoBehaviour {

    [SerializeField, Header("コンテニュー受付時間")]
    public float time=10.0f;

    [SerializeField, Header("コンテニューキー")]
    public string key;

    [SerializeField, Header("メインシーン名")]
    public string main_scene;

    [SerializeField, Header("タイトルシーン名")]
    public string title_scene;

    [SerializeField, Range(0, 1.0f), Header("alphaの値")]
    public float _alpha = 0.0f;

    [SerializeField, Header("alphaに加算する値")]
    public float plus = 0.1f;

    GameObject _text; //テキスト代入用変数

    GameObject _image;//イメージ代入用変数

    public bool timeflg=true;


    public bool  conteflg = false;

    public bool overflg = false;

    public string messege="continue?";

    public float r, g, b = 1.0f;

    float t = 0;




    // Use this for initialization
    void Start ()
    {
        _text = GameObject.Find("Text");
        _image = GameObject.Find("Image");
	}
	
	// Update is called once per frame
	void Update ()
    {

        _text.GetComponent<Text>().text =messege+" "+(int)time; //コンテニュー表示と制限時間表示

        if (timeflg==true)
        {
            time -= Time.deltaTime; //コンテニューの時間減算
        }
       

        _image.GetComponent<Image>().color = new Color(r, g, b, _alpha);


        if (conteflg == false)
        {
            _alpha += plus; //alpha値の加算
            r -= plus;
            g -= plus;
            b -= plus;
        }
       
            if (time > 0)
            {
                if (Input.GetKeyDown(key))
                {
                    conteflg = true;
                }
            }
            else
            {
                overflg = true;
            }


        if (conteflg == true)
        {
            contiue();
        }

        if(overflg==true)
        {
            Gameover();
        }

    }
      	
	

    public void contiue()  //コンテニュー時の処理
    {
        print("メソッド入りました");
        timeflg = false;
        
        _alpha -= plus;

        print(_alpha);

        r += plus;
        g += plus;
        b += plus;

        messege = "Retry OK";
        

        
        if (_alpha<=0.0f)
        {
            SceneManager.LoadScene(main_scene);
        }
    }


    public void Gameover()
    {
        time = 0.0f;
        _alpha = 0.0f;
        _text.GetComponent<Text>().color = new Color(1,0,0,1);
        messege = "Game Over";

      
        t +=Time.deltaTime;
        print(t);

        if(t>2.0f)
        {
            SceneManager.LoadScene(title_scene);
        }

    }
}
