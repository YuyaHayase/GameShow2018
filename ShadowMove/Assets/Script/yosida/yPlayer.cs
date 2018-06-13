using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class yPlayer : MonoBehaviour {

    Rigidbody2D rigi2D;

    BoxCollider2D box2D;

    [SerializeField, Header("")]
    ContactFilter2D filter2D;

    [SerializeField,Header("歩くスピード")]
    float speed;


    [Header("----落下する減速率の調整　高いほど落下する速度が速くなる----")]
    [SerializeField, Header("ジャンプの減速率")]
    float jumpAccel = 0.5f;

    [SerializeField, Header("ジャンプしたときの落下の減速率")]
    float jumpFallAccel = 0.3f;

    [SerializeField, Header("自由落下の減速率")]
    float freeFallAccel = 0.1f;

    [Header("----ジャンプ処理---")]
    [SerializeField, Header("初速度")]
    float Vo = 0.3f;

    [SerializeField, Header("時間")]
    float time;

    [SerializeField, Header("重力")]
    float Gravity = 9.8f;

    bool flgWork = false;
    bool flgBackWork = false;
    bool flgJump = false;
    bool flgFilter = false;


	// Use this for initialization
	void Start () {
        rigi2D = GetComponent<Rigidbody2D>();
        box2D = GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {
        // y = Vot + 1 / 2gt²
        //y = g * t

        float y = 0;

        //あたり判定
        flgFilter = rigi2D.IsTouching(filter2D);

        OperationKey();

        //前に歩く
        if (flgWork)
            transform.position += new Vector3(speed, 0, 0);

        //後ろに歩く
        if (flgBackWork)
            transform.position -= new Vector3(speed, 0, 0);

        //ジャンプ
        if (flgJump)
        {
            float yMax = y;

            time += Time.deltaTime;
            y = (Vo * time) - Gravity * (time * time) * jumpAccel;

            if(y < yMax)
            {
                y *= jumpFallAccel;
            }

            transform.position += new Vector3(0, y, 0);
        }
        else if (!flgFilter)
        {
            //ジャンプ以外の時の自由落下
            time += Time.deltaTime;
            y = Gravity * time * freeFallAccel;

            transform.position -= new Vector3(0, y, 0);
        }
    }

    //操作キー
    private void OperationKey()
    {
        //右に歩く
        if (Input.GetButtonDown("Horizontal"))
        {
            flgWork = true;
            flgBackWork = false;
        }

        //後ろに下がる
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            flgBackWork = true;
            flgWork = false;
        }

        //止まる
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            flgWork = false;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            flgJump = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        //地面に当たったら落下処理がなくなる
        if (coll.gameObject.CompareTag("block"))
        {
            flgJump = false;
            time = 0;
        }

        //ゲームオーバー
        if (coll.gameObject.CompareTag("GameOver"))
        {
            print("死んじゃった☆");
        }
    }
}
