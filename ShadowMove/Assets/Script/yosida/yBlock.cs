using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yBlock : MonoBehaviour {

    //------------------------------------------------
    //| 乗り移ったオブジェクトから離れたとき追加する |
    //------------------------------------------------
    Rigidbody2D rigi2D;
    BoxCollider2D box2D;

    string[] tagName = { "block", "Needle", "gimmick", "Boss" };

    float time = 0.0f;
    float Gravity;
    float freeFallAccel;

    // Use this for initialization
    void Start () {
        //Rigidbody2Dを追加する
        //rigi2D = gameObject.AddComponent<Rigidbody2D>();

        box2D = GetComponent<BoxCollider2D>();

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
                if (tagName[i] != "Boss")
                {
                    this.tag = "gimmick";
                }

                //角度が変わるからその角度を直す
                transform.rotation = Quaternion.Euler(0, 0, 0);


                //消す
                Destroy(rigi2D);
                Destroy(this);
                break;
            }
        }
    }
}
