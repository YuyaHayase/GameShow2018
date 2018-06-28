using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yPlayerManager : MonoBehaviour {

    [SerializeField, Header("画面外に出たときに表示する画像")]
    SpriteRenderer spr;

    [SerializeField, Header("Playerが移動できるx軸の最大範囲")]
    float xMax;
    [SerializeField, Header("Playerが移動できるx軸の最小範囲")]
    float xMin;

    bool flgOut = false;

	// Use this for initialization
	void Start () {
        spr = Instantiate(spr);
        spr.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        //------Playerが動ける範囲制限--------
		//if(transform.position.x >= xMax)
        //{
        //    transform.position = new Vector3(xMax, transform.position.y, 0);
        //}

        //if(transform.position.x <= xMin)
        //{
        //    transform.position = new Vector3(xMin, transform.position.y, 0);
        //}


        //-------画面外に出たら画像を出す--------
        if (flgOut)
        {
            Vector3 cameraPos;
            Vector3 cameraTopLeft = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            Vector3 cameraBottomRight = Camera.main.ScreenToWorldPoint(Vector3.zero);

            float sprPosY;

            //右に画像がでる
            if (transform.position.x > Camera.main.transform.position.x)
                cameraPos = cameraTopLeft;
            else//左に画像がでる
                cameraPos = cameraBottomRight;

            if (transform.position.y >= cameraTopLeft.y)
                sprPosY = cameraTopLeft.y;
            else if (transform.position.y <= cameraBottomRight.y)
                sprPosY = cameraBottomRight.y;
            else
                sprPosY = transform.position.y;

            spr.transform.position = new Vector3(cameraPos.x, sprPosY, 0);
            spr.enabled = true;
        }
        else
        {
            spr.enabled = false;
        }
	}

    //画面外に出たとき
    private void OnBecameInvisible()
    {
        flgOut = true;
    }

    //画面内に入ったとき
    private void OnBecameVisible()
    {
        flgOut = false;
    }

}
