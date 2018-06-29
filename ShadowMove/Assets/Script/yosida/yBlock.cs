using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yBlock : MonoBehaviour {

    //------------------------------------------------
    //| 乗り移ったオブジェクトから離れたとき追加する |
    //------------------------------------------------
    Rigidbody2D rigi2D;
    Vector2 mySize;

    string[] tagName = { "block", "Needle", "gimmick", "Boss" };

    float time = 0.0f;
    float Gravity;
    float freeFallAccel;

    bool flg = false;

    // Use this for initialization
    void Start () {
        //Rigidbody2Dを取得する
        rigi2D = GetComponent<Rigidbody2D>();

        mySize = GetComponent<SpriteRenderer>().bounds.size;

	}
	
	// Update is called once per frame
	void Update () {

        //自由落下
        time += Time.deltaTime;
        float y = Gravity * time * freeFallAccel;

        transform.position += new Vector3(0, -y, 0);

    }

    //追加したときにこれに値を入れて
    public void Config(float Gravity,float freeFallAccel)
    {
        //重力
        this.Gravity = Gravity;

        //減速率
        this.freeFallAccel = freeFallAccel;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        for (int i = 0; i < tagName.Length; i++)
        {
            if (coll.gameObject.CompareTag(tagName[i]))
            {
                if (!flg)
                {
                    flg = true;
                    if (tagName[i] != "Boss")
                    {
                        this.tag = "gimmick";
                    }

                    //角度が変わるからその角度を直す
                    //transform.rotation = Quaternion.Euler(0, 0, 0);
                    print(1);
                    Destroy(rigi2D);

                    Vector2 collSize = coll.gameObject.GetComponent<SpriteRenderer>().bounds.size;
                    Vector3 pos = transform.position;

                    pos.y = coll.gameObject.transform.position.y + (mySize.y / 2) + (collSize.y / 2);
                    transform.position = pos;
                    //消す
                    Destroy(this);
                    break;
                }
            }
        }
    }
}
