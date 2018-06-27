using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fShodow : MonoBehaviour
{
    
    
    
        [SerializeField, Header("移動スピード")]
        private float shedowWALK;
    
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

    void Start()
    {
        Statas(_HP, 2.0f, r,1);
        player = GameObject.Find("player");
        
        
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
                gameObject.SetActive(false);
                walkflg = false;
            }
        }


        if (walkflg ==false)// のりうつった物体の移動
        {

            if (Input.GetKey(rightMOVE))
            {
                obj.transform.Translate(shedowWALK * Time.deltaTime, 0, 0);
            }

            if (Input.GetKey(leftMOVE))
            {
                obj.transform.Translate(shedowWALK*-1 * Time.deltaTime, 0, 0);
            }

            if (Input.GetKey(downMOVE))
            {
                obj.transform.Translate(0,shedowWALK * -1 * Time.deltaTime, 0);
            }
            if (Input.GetKey(upMOVE))
            {
               obj.transform.Translate(0,shedowWALK * Time.deltaTime, 0);
            }

            if (Input.GetKey(actionBTN))
            {
                //rideobj = GameObject.Find("Rideobj").GetComponent<>();


            }

            if (Input.GetKeyDown(exitBTN))
            {

                walkflg = true;
                transform.position = new Vector3(obj.transform.position.x + 5.0f, transform.position.y, transform.position.z);

                GetComponent<Renderer>().material.color = new Color(0, 0, 0, 1);
            }

        }

        if (r<d)//範囲外になったら
        {
            print("来た");
            HpDame(1);

        }


    }


    public void HpDame(int Dame) //HPを減少させる処理
    {
        HP -= Dame;
        if (HP < 0) Destroy(gameObject);
        print(HP);
    }

   

    //オブジェクトに当たったら乗り移ル。
    void OnTriggerEnter2D(Collider2D other)
    {
        obj = other.gameObject;
        if(obj.name==obj_str)
        {
            walkflg = false;

            GetComponent<Renderer>().material.color = new Color(0, 0, 0, 0);
        }
        
    }
}
