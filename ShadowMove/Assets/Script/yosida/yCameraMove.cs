using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yCameraMove : MonoBehaviour {

    GameObject player;
    GameObject shodow;

    [SerializeField,Header("カメラのy座標の調整")]
    float y;

    [SerializeField,Header("カメラ移動")]
    float speed = 1.0f;

    fShodow _fShodow;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("player");
        shodow = GameObject.Find("shodow");
        _fShodow = shodow.GetComponent<fShodow>();

    }
	
	// Update is called once per frame
	void Update () {
        //transform.position = new Vector3(shodow.transform.position.x, 
        //                                 shodow.transform.position.y + y, 
        //                                 transform.position.z);

        //乗り移っている最中
        if (_fShodow.FlgPossess)
        {
            //カメラを乗り移っているオブジェクトに追従
            if (transform.position == _fShodow.obj.transform.position)
            {
                transform.position = new Vector3(_fShodow.obj.transform.position.x,
                                                 _fShodow.obj.transform.position.y + y,
                                                 transform.position.z);
            }
            else//カメラをオブジェクトの位置までだんだん移動する
            {
                float x = Mathf.MoveTowards(transform.position.x, _fShodow.obj.transform.position.x, speed);
                float y = Mathf.MoveTowards(transform.position.y, _fShodow.obj.transform.position.y, speed);
                transform.position = new Vector3(x, y, transform.position.z);
            }
        }
        else//乗り移っていない
        {
            //カメラを影に追従
            if (transform.position == shodow.transform.position)
            {
                transform.position = new Vector3(shodow.transform.position.x,
                                                 shodow.transform.position.y + y,
                                                 transform.position.z);
            }
            else//カメラを影の位置までだんだん移動する
            {
                float x = Mathf.MoveTowards(transform.position.x, shodow.transform.position.x, speed);
                float y = Mathf.MoveTowards(transform.position.y, shodow.transform.position.y, speed);
                transform.position = new Vector3(x, y, transform.position.z);
            }

        }
    } 
}
