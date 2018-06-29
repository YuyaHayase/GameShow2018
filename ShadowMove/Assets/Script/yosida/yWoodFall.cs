using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yWoodFall : MonoBehaviour {

    [SerializeField, Header("倒れる方向　左…１　右…２")]
    [Range(1,2)]
    int fallDir = 1;

    [SerializeField,Header("倒れるスピード")]
    float dirSpeed = 0.5f;

    [SerializeField, Header("倒したい角度")]
    [Range(0,360)]
    float angle = 90.0f;

    [SerializeField,Header("デバッグ用(倒れる)")]
    bool flgFall = false;

    public bool FlgFall
    {
        get { return flgFall; }
        set { flgFall = value; }
    }

	// Use this for initialization
	void Start () {
        //右に倒れる場合マイナスになる
        if (fallDir == 2)
            dirSpeed = -dirSpeed;

	}
	
	// Update is called once per frame
	void Update () {
        
        if (flgFall)
        {
            transform.Rotate(0, 0, dirSpeed);

            //倒れる方向を決める
            if (fallDir == 1)
                LeftFall();
            else
                RightFall();
        }
	}

    // 左に倒れる
    private void LeftFall()
    {
        if(transform.localEulerAngles.z >= angle)
        {
            flgFall = false;
            transform.rotation = Quaternion.Euler
                (transform.eulerAngles.x, transform.eulerAngles.y, angle);

            angle += 90;
            if (angle > 180.0f)
            {
                angle = 0.0f;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
    //右に倒れる
    private void RightFall()
    {
        if (transform.localEulerAngles.z <= angle)
        {
            flgFall = false;
            transform.rotation = Quaternion.Euler
                (transform.eulerAngles.x, transform.eulerAngles.y, angle);

            angle -= 90;
            if (angle < 180.0f)
            {
                angle = 360.0f;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
}
