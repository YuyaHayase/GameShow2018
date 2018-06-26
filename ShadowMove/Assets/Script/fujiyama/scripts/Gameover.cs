using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour {

    [SerializeField, Header("コンテニュー時間制限")]
    public float  contTime = 10;

    [SerializeField, Header("ゲームメインシーン名")]
    public string Main_name;

    [SerializeField, Header("タイトルシーン名")]
    public string title_name;

    [SerializeField, Header("コンテニューキー")]
    public string key_choise;

    [SerializeField, Header("白")]
    public Color white;

    [SerializeField, Header("黒")]
    public Color black;

    Image img;
    [SerializeField,Header("RGB"),Range(0,1.0f)]
    public float red, green, blue;

    [SerializeField, Header("変化割合"), Range(0, 1.0f)]
    public float t;

    Camera camera;
    hBlur hBlur;


    float cnt;




    // Use this for initialization
    void Start ()
    {
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        hBlur = Camera.main.GetComponent<hBlur>();
        img = GameObject.Find("Image").GetComponent<Image>();
        //red = img.GetComponent<Image>().color.r;
        //green = img.GetComponent<Image>().color.g;
        //blue = img.GetComponent<Image>().color.b;
    }
	
	// Update is called once per frame
	void Update ()
    {
        cnt += 0.01f;
        //t += 0.078f*Time.deltaTime;
        //red = (1.0f - t) * white.r + t * black.r;
        //green = (1.0f - t) * white.g + t * black.g;
        //blue = (1.0f - t) * white.b + t * black.b;


        //img.GetComponent<Image>().color = new Color(red, green, blue, 0);
           Debug.Log(cnt);
        
            hBlur.BlurChange(hBlur.PlusMinus.plus);
        


        this.GetComponent<Text>().text = "continue? " + (int)contTime+"\n";
        contTime -=Time.deltaTime;

        if(contTime>0) //
        {
            if(Input.GetKeyDown(key_choise))
            {
                //SceneManager.LoadScene(Main_name);
            }
        }

        else
        {
            contTime = 0;
            //SceneManager.LoadScene(title_name);
        }
       
		
	}
}
