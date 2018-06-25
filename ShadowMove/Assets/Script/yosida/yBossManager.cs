using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yBossManager : MonoBehaviour {

    SpriteRenderer mySpr;       //自身のSpriteRenderer

    [SerializeField, Header("最後の顔色")]
    Color endColor;
    Color startColor;

    [SerializeField, Header("ボスのHP")]
    float hp;
    float maxHP;                //ボスの最大HP
    float remaining;            //自身のHPを0～1の範囲で

    [SerializeField, Header("線形補完の減速率")]
    float accele = 0.2f;
    float t = 0.0f;             //線形補完の為

    bool flgDamage = false;     //ダメージを受けたかどうか
    
	// Use this for initialization
	void Start () {
        //自身の画像や色を取得
        mySpr = GetComponent<SpriteRenderer>();
        startColor = GetComponent<SpriteRenderer>().color;

        //最大HPを入れる
        maxHP = hp;
	}
	
	// Update is called once per frame
	void Update () {

        //デバッグ用
        if (Input.GetKeyDown(KeyCode.Space))
            BossDamage(10);

        //ダメージを受けたら
        if(flgDamage)
        {
            //ダメージに応じて徐々に顔色が変わっていく
            if(t < remaining)
            {
                t += Time.deltaTime * accele;
            }
            else
            {
                flgDamage = false;
                t = remaining;
            }
            
        }
        
        //顔色の線形補完
        float red = (1.0f - t) * startColor.r + t * endColor.r;
        float green = (1.0f - t) * startColor.g + t * endColor.g;
        float blue = (1.0f - t) * startColor.b + t * endColor.b;

        mySpr.color = new Color(red, green, blue);

    }

    //ボスがダメージを受けたら
    private void BossDamage(int damage)
    {
        //顔色の線形補完をする
        flgDamage = true;

        //HPを減らす
        hp -= damage;
        if (hp < 0)
            hp = 0;

        //自身のHPを0～1の範囲で(線形補完をするため)
        remaining = 1.0f - (hp / maxHP);
    }
}
