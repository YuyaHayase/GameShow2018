using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fgameover : MonoBehaviour {

    [SerializeField, Header("GameOverの文字")]
    public Sprite[] gameover = new Sprite[6];

    GameObject[] _moji = new GameObject[8];

    [SerializeField,Range(0,1.0f),Header("aplhaに減算する値")]
    public float _aplha;

    [SerializeField, Range(0, 1.0f), Header("aplhaに加算する値")]
    public float _aplha2;

    [SerializeField,Header("添え字")]
    private int i=0;

    [SerializeField,Header("非出現aplha")]
    float aplha = 1;
    [SerializeField, Header("出現aplha")]
    float aplha2 = 0;
    [SerializeField, Header("メインシーン")]
    public string mainscene;
    [SerializeField, Header("タイトルシーン")]
    public string titlescene;

    public float time = 0;

    Text tx;

    bool flg = true;


    // Use this for initialization



    void Start ()
    {
        _moji[0] = GameObject.Find("Canvas/Image");
        _moji[1] = GameObject.Find("Canvas/Image(1)");
        _moji[2] = GameObject.Find("Canvas/Image(2)");
        _moji[3] = GameObject.Find("Canvas/Image(3)");
        _moji[4] = GameObject.Find("Canvas/Image(4)");
        _moji[5] = GameObject.Find("Canvas/Image(5)");
        _moji[6] = GameObject.Find("Canvas/Image(6)");
        _moji[7] = GameObject.Find("Canvas/Image(7)");
        tx = GameObject.Find("Canvas/Text").GetComponent<Text>();



    }
	
	// Update is called once per frame
	void Update ()
    {



        switch (i)
        {
            case 0:
                _moji[0].GetComponent<Image>().color = new Color(1, 1, 1, aplha);
                break;
            case 1:
                _moji[0].GetComponent<Image>().sprite = gameover[0];
                _moji[0].GetComponent<Image>().color = new Color(1, 1, 1, aplha2);
                _moji[1].GetComponent<Image>().color = new Color(1, 1, 1, aplha);
                break;
            case 2:
                _moji[1].GetComponent<Image>().sprite = gameover[1];
                _moji[1].GetComponent<Image>().color = new Color(1, 1, 1, aplha2);
                _moji[2].GetComponent<Image>().color = new Color(1, 1, 1, aplha);
                break;
            case 3:
                _moji[2].GetComponent<Image>().sprite = gameover[2];
                _moji[2].GetComponent<Image>().color = new Color(1, 1, 1, aplha2);
                _moji[3].GetComponent<Image>().color = new Color(1, 1, 1, aplha);
                break;
            case 4:
                _moji[3].GetComponent<Image>().sprite = gameover[3];
                _moji[3].GetComponent<Image>().color = new Color(1, 1, 1, aplha2);
                _moji[4].GetComponent<Image>().color = new Color(1, 1, 1, aplha);
                break;
            case 5:
                _moji[4].GetComponent<Image>().sprite = gameover[4];
                _moji[4].GetComponent<Image>().color = new Color(1, 1, 1, aplha2);
                _moji[5].GetComponent<Image>().color = new Color(1, 1, 1, aplha);
                break;
            case 6:
                _moji[5].GetComponent<Image>().sprite = gameover[5];
                _moji[5].GetComponent<Image>().color = new Color(1, 1, 1, aplha2);
                _moji[6].GetComponent<Image>().color = new Color(1, 1, 1, aplha);
                break;
            case 7:
                _moji[6].GetComponent<Image>().sprite = gameover[6];
                _moji[6].GetComponent<Image>().color = new Color(1, 1, 1, aplha2);
                _moji[7].GetComponent<Image>().color = new Color(1, 1, 1, aplha);
                break;
            case 8:
                _moji[7].GetComponent<Image>().sprite = gameover[7];
                _moji[7].GetComponent<Image>().color = new Color(1, 1, 1, aplha2);
                tx.enabled = false;
                Camera.main.backgroundColor = new Color(0, 0, 0, 1);

                
                break;
            default:
                time += Time.deltaTime;

                if (time > 2.0f) SceneManager.LoadScene(titlescene);
                flg = false;
                print("sub");
                break;
        }

        aplha -= _aplha;//*Time.deltaTime;

        aplha2 += _aplha2;

        if (aplha <= 0.0f)
        {
            aplha = 1.0f;
            aplha2 = 0.0f;
            i++;
            print("A");
        }



        if (flg == true)
        {
            if (Input.GetKey(KeyCode.A))
            {

                {
                    print("main");
                    SceneManager.LoadScene(mainscene);
                }


            }

        }







    }
}
