using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yPlayerAI2 : MonoBehaviour
{

    //Rayが飛ばされる場所
    GameObject rayHitPointUnder;//次歩くところにblockがあるかの判定場所
    GameObject rayHitPointHeight;//次歩くところが高いかどうかの判定場所

    [SerializeField]
    ContactFilter2D filter2d;

    Rigidbody2D rigi2d;

    [SerializeField, Header("歩くスピード")]
    float speed = 0.1f;

    int frame = 0;

    //座標
    float yPosSave;

    //テーブル利用フレーム
    float[] jumpTable;

    bool flgContact = false;//接触判定
    bool flgJump = false;//ジャンプ
    bool flgWork = false;//歩く


    // Use this for initialization
    void Start()
    {
        //子オブジェクトの取得
        rayHitPointUnder = transform.Find("RayHitPoint_Under").gameObject;
        rayHitPointHeight = transform.Find("RayHitPoint_Height").gameObject;


        rigi2d = GetComponent<Rigidbody2D>();

        jumpTable = new float[]
        {
            0.1f,0.1f,0.1f,0.1f,
            0.3f,0.3f,0.3f,0.3f,
            0.5f,0.5f,0.5f,0.5f,
            0.3f,0.3f,0.3f,0.3f,
            0.1f,0.1f,0.1f,0.1f,
            0, 0, 0, 0,
            -0.2f,-0.2f,-0.2f,-0.2f,
            -0.5f,-0.5f,-0.5f,-0.5f,
            -0.8f,-0.8f,-0.8f,-0.8f
        };
    }

    // Update is called once per frame
    void Update()
    {
        //PlayeroOperation();

        flgContact = rigi2d.IsTouching(filter2d);
        print(flgContact);

        //Rayでhitしたオブジェクトを全て取得
        RaycastHit2D[] hitObjectUnder = Physics2D.RaycastAll(rayHitPointUnder.transform.position, Vector2.zero);
        RaycastHit2D[] hitObjectHeight = Physics2D.RaycastAll(rayHitPointHeight.transform.position, Vector2.zero);

        //Rayで何かを取得したら
        if (hitObjectUnder.Length > 0)
        {
            //tagがBlockだった場合歩き続ける
            if (hitObjectUnder[TagNum(hitObjectUnder, "block")].collider.gameObject.CompareTag("block"))
                flgWork = true;
            else
                flgWork = false;
        }
        else
        {
            flgWork = false;
        }

        if (flgContact)
        {
            if (hitObjectHeight.Length > 0)
            {
                //一歩先にブロックがあったら止まる
                if (hitObjectHeight[TagNum(hitObjectHeight, "block")].collider.gameObject.CompareTag("block"))
                    flgJump = true;
                else
                    flgJump = false;
            }
        }

        if (flgWork)
        {
            transform.position += new Vector3(speed, 0, 0);
        }

    }

    private void FixedUpdate()
    {
        if (flgJump)
        {
            rigi2d.AddForce(Vector2.up * 250.0f);

            flgJump = false;
        }

    }

    //デバッグ用　自身で操作できる
    private void PlayeroOperation()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(speed, 0, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-speed, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            flgJump = true;
            frame = 0;
        }

        if (flgJump)
        {
            transform.position += new Vector3(0, jumpTable[frame], 0);
            frame++;

            if (frame >= jumpTable.Length)
            {
                flgJump = false;
                transform.position = new Vector3(transform.position.x, yPosSave, 0);
            }
        }

    }
    //指定したTagを探して配列番号を返す
    private int TagNum(RaycastHit2D[] hitObject, string tagName)
    {
        for (int i = 0; i < hitObject.Length; i++)
        {
            if (hitObject[i].collider.gameObject.CompareTag(tagName))
            {
                return i;
            }
        }

        return 0;
    }

}
