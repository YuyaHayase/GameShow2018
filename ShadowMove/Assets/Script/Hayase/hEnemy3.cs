using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class hEnemy3 : hEnemy {

    [SerializeField, Header("プレイヤーを気づく範囲")]
    float NoticePlayerDistance = 10.0f;

    [SerializeField, Header("ベクタ線の一時的保存座標")]
    Vector2 Vpos;

    [SerializeField, Header("線形補間")]
    float t = 0;
    [SerializeField, Header("線形補間追加")]
    float addT = 0.025f;
    [SerializeField, Header("線形補間係数")]
    int accelAdd = 2;

    // 一時停止カウント
    float cnt = 0;

    //　帰るか
    bool goback = false;

    // Use this for initialization
    void Start () {
        pos = transform.position;
        EnemyState = Status.Wait;
        if(player == null) player = GameObject.Find("player");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        CharacterStatus(EnemyState);
	}

    public override void CharacterStatus(Status status)
    {
        switch (status)
        {
            // 攻撃
            case Status.Attack:
                break;

            // 移動
            case Status.Move:
                if (!goback)
                {
                    if (t < 0.8f)t += addT;

                    if (t > 0.8f) goback = true;
                }
                else
                {
                    cnt += 0.01f;
                    if (cnt > 2f)
                        t -= addT;
                    if (t < 0)
                    {
                        cnt = 0;
                        goback = false;
                        DeleteEnemy();
                    }
                }
                Vector2 calc = new Vector2(Vpos.x, player.transform.position.y);
                transform.position = Mathf.Pow((1-t),accelAdd) * Vpos + 2 * t * (1 - t) * calc + Mathf.Pow(t,accelAdd) * (Vector2)player.transform.position;
                
                pos = transform.position;
                break;

            // 待ち
            case Status.Wait:
                if ( player != null && (player.transform.position - transform.position).magnitude < NoticePlayerDistance)
                {
                    EnemyState = Status.Move;
                    Vpos = transform.position;
                }
                break;
        }
    }

}
