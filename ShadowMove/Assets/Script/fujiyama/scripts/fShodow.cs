using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fShodow : MonoBehaviour
{
    
    
    struct Status
    {
        [SerializeField, Header("移動スピード")]
        private float shedowWALK;
        public float get_shedowWALK()
        {
            return shedowWALK;
        }

        public GameObject objct;


        [SerializeField, Header("勇者オブジェクト取得用")]
        public GameObject player;


        [SerializeField, Header("影の移動制限の値")]
        public float r;
        public float get_r()
        {
            return r;
        }

        [SerializeField, Header("HP")]
        public int HP;
        
        public int _HPsetter
        {
            get { return HP; }
            set { HP = value; }
        }

        [SerializeField, Header("ダメージ量")]
        public int Dame;

        public int get_Dame()
        {
            return Dame;
        }

        public Vector2 shodow_point;//　影のベクトル

        public Vector2 player_point;//  勇者のベクトル 

       

        public void Statas(int _HP,float _shedowwalk,int _r, int _Dame)
        {
            HP = _HP;
            shedowWALK = _shedowwalk;
            r = _r;
            Dame = _Dame;
        }

    }

    private static bool walkflg = true;

    private string judge;

    [SerializeField, Header("右移動")]
    private string rightMOVE;

    [SerializeField, Header("左移動")]
    private string leftMOVE;

    [SerializeField, Header("アクションボタン")]
    private string actionBTN;

    [SerializeField, Header("脱出ボタン")]
    private string exitBTN;

    


    Status sta = new Status(); //アクセス修飾詞
    public GameObject obj;  //

    public GameObject player;

    int a;
   

    void Start()
    {
        sta.Statas(10, 2.0f, 10,1);
        player = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {

        sta.shodow_point = transform.position;
        sta.player_point=player.transform.position;　//自分のベクトル代入

        Vector2 dir =sta.player_point -sta.shodow_point; //勇者ベクトル-自分ベクトル代入

        float d = dir.magnitude;

        



        if (walkflg)
        {
            //移動処理
            if (Input.GetKey(rightMOVE))
            {
                transform.Translate(sta.get_shedowWALK() * Time.deltaTime, 0, 0);
            }


            if (Input.GetKey(leftMOVE))
            {
                transform.Translate(sta.get_shedowWALK()*-1 * Time.deltaTime, 0, 0);
            }
            //action行動
            if (Input.GetKeyDown(actionBTN))
            {
                gameObject.SetActive(false);
                walkflg = false;
            }
        }


        if (walkflg ==false)// のりうつった物体の移動
        {

            if (Input.GetKey(rightMOVE))
            {
                obj.transform.Translate(sta.get_shedowWALK() * Time.deltaTime, 0, 0);
            }

            if (Input.GetKey(leftMOVE))
            {
                obj.transform.Translate(sta.get_shedowWALK()*-1 * Time.deltaTime, 0, 0);
            }


            if (Input.GetKey(actionBTN))
            {
                //物体ごとのメソッドを呼ぶ
                //switch(obj.name)

            }

            if (Input.GetKeyDown(exitBTN))
            {

                walkflg = true;
                gameObject.SetActive(true);
            }

        }

        if (sta.r<d)//範囲外になったら
        {
            print("来た");
            HpDame(1);

        }


    }


    public void HpDame(int Dame) //HPを減少させる処理
    {
        sta.HP -= Dame;
        if (sta.HP < 0) Destroy(gameObject);
        print(sta.HP);
    }

   

    //オブジェクトに当たったら乗り移ル。
    void OnTriggerEnter2D(Collider2D other)
    {
        obj = other.gameObject;
        walkflg = false;
    }
}
