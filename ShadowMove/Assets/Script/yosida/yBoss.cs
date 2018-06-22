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

    [SerializeField,Header("攻撃パターン")]
    List<int> pattern = new List<int>();
    List<int> patternSave = new List<int>();  //攻撃パターンの保存

    [SerializeField, Header("吐き出す攻撃回数")]
    int outNum = 3;
    int order = 1;          //攻撃の順番
    int r;                  //ランダムでもらった値

    [SerializeField,Header("クールタイム")]
    float nonCoolTime;
    float coolTime;

    [SerializeField, Header("吐き出す攻撃のクールタイム")]
    float nonOutTime = 2.0f;


    [SerializeField,Header("アニメーションの速度")]
    float animeSpeed = 1.0f;

    [SerializeField,Header("歩く速度")]
    float speed = 0.1f;

    [SerializeField, Header("動けるx軸の最小値")]
    float min;
    [SerializeField, Header("動けるx軸の最大値")]
    float max;

    bool flgRand = true;

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

        if(coolTime >= nonCoolTime)
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
                animeSpeed = 15.0f;
                //右に移動
                if (player.transform.position.x > transform.position.x)
                    speed = Mathf.Abs(speed);
                else//左に移動
                    speed = -speed;
                order = 2;
                break;
            case 2://画面端まで移動する
                transform.position += new Vector3(speed, 0, 0);
                if (transform.position.x > max || transform.position.x < min)
                    order = 3;
                break;
            case 3://攻撃終わり
                animeSpeed = 1.0f;
                AttackEnd();
                break;
        }
        
    }

    //口から何か出す
    private void OutAttack()
    {
        for (int i = 0; i < outNum; i++)
        {
            GameObject go = Instantiate(outPrefab, ChildOutPos.transform.position, Quaternion.identity);
        }
        AttackEnd();
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

    /*
     ボスの攻撃パターン
     * ふみつける  
    // * たいあたり
    //* 口から何か出す
     * ジャンプ
     * 仲間呼び
     */
}
