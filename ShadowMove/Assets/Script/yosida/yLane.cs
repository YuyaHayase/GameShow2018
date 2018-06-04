using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
class yObjectMove
{
    [SerializeField,Header("動きたい場所を指定")]
    Vector2 moveSpeed;
    [SerializeField,Header("時間")]
    float time;
}

public class yLane : MonoBehaviour {

    [SerializeField,Header("0…x軸の移動/1…y軸の移動")]
    [Range(0,1)]
    int axis;
    int mode;

    [SerializeField,Header("最小値")]
    float min;
    [SerializeField, Header("最大値")]
    float max;

    float speed = 0.1f;

	// Use this for initialization
	void Start () {
        if (axis == 0)
            mode = 1;
        else
            mode = 3;
	}
	
	// Update is called once per frame
	void Update () {
        switch (mode)
        {
            case 1://右移動
                RectTransformX(speed);
                if (transform.localPosition.x > max)
                    mode = 2;
                    break;
            case 2://左移動
                RectTransformX(-speed);
                if (transform.localPosition.x < min)
                    mode = 1;
                break;
            case 3://上移動
                RectTransformY(speed);
                if (transform.localPosition.y > max)
                    mode = 4;
                break;
            case 4://下移動
                RectTransformY(-speed);
                if (transform.localPosition.y < min)
                    mode = 3;
                break;
        }
    }

    private void RectTransformX(float speed)
    {
        transform.localPosition += new Vector3(speed, 0, 0);
    }
    private void RectTransformY(float speed)
    {
        transform.localPosition += new Vector3(0, speed, 0);
    }

}
