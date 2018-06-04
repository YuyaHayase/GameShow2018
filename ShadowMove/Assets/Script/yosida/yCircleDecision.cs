using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yCircleDecision : MonoBehaviour {

    [SerializeField]
    SpriteRenderer player;

    SpriteRenderer mySpr;

    Rigidbody2D rigi2D;
    Vector2 pos;

    [Header("--------------------確認用-----------------------")]
    [SerializeField]
    float r;
    [SerializeField]
    float objR;
    public float ySave;

    public bool flg = false;

    // Use this for initialization
    void Start () {
        //自身のオブジェクトを取得
        mySpr = GetComponent<SpriteRenderer>();

        //自身のオブジェクトのサイズを取得して半径を求める
        r = mySpr.bounds.size.x / 2;
        objR = GetComponent<SpriteRenderer>().bounds.size.x / 2;
        rigi2D = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        //自身の位置を取得
        pos = transform.position;

        //三平方の定理で距離を求める
        float x = (player.transform.position.x - pos.x);
        float y = (player.transform.position.y - pos.y);
        float radius = objR + r;

        if ((x * x) + (y * y) <= (radius * radius))
        {
            //print("atari");
            ySave = transform.position.y;
            flg = true;
        }




	}
}
