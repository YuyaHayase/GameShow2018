using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class yBoss : MonoBehaviour {

    GameObject player;
    GameObject ChildOutPos;
    [SerializeField,Header("吐き出すオブジェクト")]
    GameObject outPrefab;
    Animator anime;

    Vector3 playerPos = new Vector3();

    [Header("０…体当たり １…吐き出し攻撃 ２…ジャンプ攻撃")]
    [SerializeField,Header("攻撃パターン")]
    List<int> pattern = new List<int>();
    List<int> patternSave = new List<int>();  //攻撃パターンの保存

    [SerializeField, Header("吐き出す攻撃回数")]
    int notOutNum = 3;
    int outNum = 0;
    int order = 1;          //攻撃の順番
    int r;                  //ランダムでもらった値

    [SerializeField,Header("クールタイム")]
    float notCoolTime;
    float coolTime = 0.0f;

    [SerializeField, Header("吐き出す攻撃のクールタイム")]
    float notOutTime = 2.0f;
    float outTime = 0.0f;


    [SerializeField,Header("アニメーションの速度")]
    float animeSpeed = 1.0f;

    [SerializeField,Header("歩く速度")]
    float speed = 0.1f;

    [SerializeField, Header("動けるx軸の最小値")]
    float xMin;
    [SerializeField, Header("動けるx軸の最大値")]
    float xMax;
    [SerializeField, Header("ジャンプするときのy軸の最大座標")]
    float yMax;

    bool flgRand = true;
    bool flgJumpStop = false;

	// Use this for initialization
	void Start () {
        //オブジェクトを取得
        player = GameObject.Find("player");
        ChildOutPos = transform.Find("outPos").gameObject;

        //自身のアニメーションを取得
        anime = GetComponent<Animator>();

        //攻撃パターンを他の変数に保存する
        for(int i = 0;i < pattern.Count;i++)
            patternSave.Add(pattern[i]);
	}
	
	// Update is called once per frame
	void Update () {
        coolTime += Time.deltaTime;

        if(coolTime >= notCoolTime)
        {
            //攻撃パターンが無くなったらを入れなおす
            if(pattern.Count == 0)
            {
                for (int i = 0; i < patternSave.Count; i++)
                    pattern.Add(patternSave[i]);
            }

            //攻撃パターンの配列からランダムで取得
            if (flgRand)
            {
                flgRand = false;
                r = Random.Range(0, pattern.Count);
            }

            switch (pattern[r])
            {
                case 0://体当たり
                    BodyBlow();
                    break;
                case 1://吐き出し攻撃
                    OutAttack();
                    break;
                case 2://ジャンプ攻撃
                    JumpAttack();
                    break;
                default://次の攻撃へ
                    AttackEnd();
                    break;
            }


        }

        //アニメーションの速度
        anime.speed = animeSpeed;

    }

    //体当たり攻撃
    private void BodyBlow()
    {
        switch (order)
        {
            case 1://左右のどっちに移動するか
                animeSpeed += 0.1f;
                if (animeSpeed >= 15.0f)
                {
                    animeSpeed = 15.0f;
                    //右に移動
                    if (player.transform.position.x > transform.position.x)
                        speed = Mathf.Abs(speed);
                    else//左に移動
                        speed = -speed;
                    order = 2;
                }
                break;
            case 2://画面端まで移動する
                transform.position += new Vector3(speed, 0, 0);
                //右に移動の時
                if(speed > 0)
                {
                    if (transform.position.x > xMax)
                        order = 3;
                }
                else//左に移動の時
                {
                    if (transform.position.x < xMin)
                        order = 3;
                }
                break;
            case 3://攻撃終わり
                animeSpeed = 1.0f;
                AttackEnd();
                break;
        }
        
    }

    //口から吐瀉物出す
    private void OutAttack()
    {
        outTime += Time.deltaTime;

        if (outTime >= notOutTime)
        {
            Instantiate(outPrefab, ChildOutPos.transform.position, Quaternion.identity);
            outTime = 0.0f;
            outNum++;
        }

        if (outNum >= notOutNum)
        {
            outNum = 0;
            AttackEnd();
        }
    }

    //ジャンプ攻撃
    private void JumpAttack()
    {
        switch (order)
        {
            case 1://Playerの座標を取得
                playerPos = player.transform.position;
                animeSpeed = 0;
                order = 2;
                break;
            case 2://ジャンプ
                transform.position += new Vector3(0, 0.1f, 0);
                if (transform.position.y >= yMax)
                {
                    transform.position = new Vector3(playerPos.x, yMax, 0);
                    flgJumpStop = false;
                    order = 3;
                }
                break;
            case 3://降りてくる
                transform.position += new Vector3(0, -0.1f, 0);
                if (flgJumpStop)
                {
                    animeSpeed = 1.0f;
                    AttackEnd();
                    flgJumpStop = false;
                }
                break;
        }
    }

    //攻撃が終わった時に呼ぶと次の攻撃をすることができる
    private void AttackEnd()
    {
        //クールタイムを初期値に戻す
        coolTime = 0;

        //攻撃パターンを消す
        pattern.RemoveAt(r);

        //攻撃パターンをランダムでできるようにする
        flgRand = true;

        //攻撃の順番を元に戻す
        order = 1;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("block"))
        {
            flgJumpStop = true;
        }
    }

    /*
     ボスの攻撃パターン
    // * ふみつける  
    // * たいあたり
    // * 口から何か出す
    // * ジャンプ
     * 仲間呼び
     */
}
