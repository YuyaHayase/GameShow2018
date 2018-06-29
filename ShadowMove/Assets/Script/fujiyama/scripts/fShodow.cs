using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fShodow : MonoBehaviour
{
    
    
    
        [SerializeField, Header("移動スピード")]
        public float shedowWALK;
        
        [SerializeField,Header("移動スピード設定するやつ")]
        public float waik;
    
        public GameObject objct;


        [SerializeField, Header("勇者オブジェクト取得用")]
        public GameObject player;


        [SerializeField, Header("影の移動制限の値")]
        public float r;
       

        [SerializeField, Header("HP")]
        public int HP;


        [SerializeField, Header("HP設定")]
        public int _HP;
        [SerializeField, Header("ダメージ量")]
        public int Dame;

        private Vector2 shodow_point;//　影のベクトル

        private Vector2 player_point;//  勇者のベクトル 

    hBlur _hBlur;

       

        public void Statas(int _HP,float _shedowwalk,float _r, int _Dame)
        {
            HP = _HP;
            shedowWALK = _shedowwalk;
            r = _r;
            Dame = _Dame;
        }

   

    private static bool walkflg = true;

    private string judge;

    [SerializeField, Header("右移動")]
    public string rightMOVE;

    [SerializeField, Header("左移動")]
    public string leftMOVE;

    [SerializeField, Header("上移動")]
    public string upMOVE;

    [SerializeField, Header("下移動")]
    public string downMOVE;

    [SerializeField, Header("アクションボタン")]
    public string actionBTN;

    [SerializeField, Header("脱出ボタン")]
    public string exitBTN;
    [SerializeField,Header("乗り移りオブジェクト")]
    public GameObject obj;

    int a;

    [SerializeField, Header("乗り移るオブジェクト名")]
    public string obj_str;

    bool flgPossess = false;

    public bool FlgPossess
    {
        get { return flgPossess; }
    }

    void Start()
    {
        Statas(_HP,waik, r,1);
        player = GameObject.Find("player");

        //_hBlur = Camera.main.GetComponent<hBlur>();

    } 

    // Update is called once per frame
    void Update()
    {

        shodow_point = transform.position;//自分のベクトル代入
        player_point =player.transform.position;　//勇者ベクトル代入

        Vector2 dir =player_point -shodow_point; //勇者ベクトル-自分ベクトル代入

        float d = dir.magnitude;

        



        if (walkflg)
        {
            //移動処理
            if (Input.GetKey(rightMOVE))
            {
                transform.Translate(shedowWALK * Time.deltaTime, 0, 0);
            }


            if (Input.GetKey(leftMOVE))
            {
                transform.Translate(shedowWALK*-1 * Time.deltaTime, 0, 0);
            }

            if (Input.GetKey(upMOVE))
            {
                transform.Translate(0,shedowWALK* Time.deltaTime, 0);
            }

            if (Input.GetKey(downMOVE))
            {
                transform.Translate(0,shedowWALK* -1 * Time.deltaTime, 0);
            }
            //action行動
            if (Input.GetKeyDown(actionBTN))
            {
                if (flgPossess)
                {
                    gameObject.SetActive(false);
                    walkflg = false;
                }
            }
        }


        if (walkflg ==false)// のりうつった物体の移動
        {

            if (Input.GetKey(rightMOVE))
            {
                obj.transform.localPosition += new Vector3(shedowWALK * Time.deltaTime, 0, 0);
            }

            if (Input.GetKey(leftMOVE))
            {
                obj.transform.localPosition += new Vector3(shedowWALK*-1 * Time.deltaTime, 0, 0);
            }

            if (Input.GetKey(downMOVE))
            {
                obj.transform.localPosition += new Vector3(0,shedowWALK * -1 * Time.deltaTime, 0);
            }
            if (Input.GetKey(upMOVE))
            {
               obj.transform.localPosition += new Vector3(0,shedowWALK * Time.deltaTime, 0);
            }

            if (Input.GetKey(actionBTN))
            {
                //rideobj = GameObject.Find("Rideobj").GetComponent<>();
                if (flgPossess)
                {
                    obj.GetComponent<yWoodFall>().FlgFall = true;
                }

            }

            if (Input.GetKeyDown(exitBTN))
            {

                walkflg = true;

                GetComponent<Renderer>().material.color = new Color(0, 0, 0, 1);
                GetComponent<BoxCollider2D>().enabled = true;


                if (flgPossess)
                {
                    Vector3 pos = obj.transform.position;

                    pos.y += 0.3f;
                    obj.transform.position = pos;


                    pos.y += 3.0f;
                    transform.position = pos;


                    //脱出した時スクリプトを追加する（ブロックが落下する、落として攻撃）
                    yBlock _yBlock = obj.gameObject.AddComponent<yBlock>();

                    //重力と減速率の設定
                    _yBlock.Config(9.8f, 0.07f);

                    //タグを変える
                    obj.tag = "AttackBlock";

                    //乗り移りやめました
                    flgPossess = false;
                }
            }

        }

        if (r<d)//範囲外になったら
        {
            //print("来た");
            HpDame(1);
           

        }


    }


    public void HpDame(int Dame) //HPを減少させる処理
    {

        HP -= Dame;

        

        if (HP < 0)
        {
            Destroy(gameObject);
            //_hBlur.BlurChange(hBlur.PlusMinus.plus);
        }
        
        //print(HP);
    }

   

    //オブジェクトに当たったら乗り移ル。
    void OnCollisionEnter2D(Collision2D other)
    {
        

        if (other.gameObject.tag == obj_str)
        {
            if (!flgPossess)
            {
                obj = other.gameObject;

                walkflg = false;

                //ただいま乗り移っています
                flgPossess = true;

                //乗り移っていたオブジェクトのトリガーをfalseにする
                obj.GetComponent<BoxCollider2D>().isTrigger = false;

                //あたり判定をつけるため
                obj.gameObject.AddComponent<Rigidbody2D>();

                //回転しないように
                obj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

                //重力をなくす
                obj.GetComponent<Rigidbody2D>().gravityScale = 0;

                //コライダーをfalseにする
                GetComponent<BoxCollider2D>().enabled = false;

                //自身の色をなくす
                GetComponent<Renderer>().material.color = new Color(0, 0, 0, 0);
            }
        }
    }
}
