using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//　NBird
class hEnemy3 : hEnemy {

    [SerializeField, Header("プレイヤーを気づく範囲")]
    float NoticePlayerDistance = 15.0f;

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

    // 動き出すまでのカウント
    float awakeTime = 0;

    //　帰るか
    bool goback = false;

    // Use this for initialization
    void Start () {
        // 初期座標代入
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
            // 移動
            case Status.Move:
                // エネミーの移動 -> プレイヤーの近くへ行き、帰る
                if (!goback)
                {
                    // 線形補間
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

                // プレイヤーとエネミーの間の適当な座標を取る
                Vector2 calc = new Vector2(Vpos.x, player.transform.position.y);
                transform.position = Mathf.Pow((1-t),accelAdd) * Vpos + 2 * t * (1 - t) * calc + Mathf.Pow(t,accelAdd) * (Vector2)player.transform.position;

                pos = transform.position;
                break;

            // 待ち
            case Status.Wait:
                if ( player != null && (player.transform.position - transform.position).magnitude < NoticePlayerDistance)
                {
                    awakeTime += addT;
                    if(awakeTime > 5)
                    {
                        awakeTime = 0;
                        EnemyState = Status.Move;
                        Vpos = transform.position;
                    }
                }
                break;
        }
    }

}
