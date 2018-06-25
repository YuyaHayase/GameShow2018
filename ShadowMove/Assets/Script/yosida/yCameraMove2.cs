using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yCameraMove2 : MonoBehaviour {

    //--------------
    //|  使わない  |
    //--------------
    GameObject player;
    GameObject shodow;

    [SerializeField,Header("カメラが追従できる最小値")]
    float min;
    [SerializeField,Header("")]
    float max;

    [SerializeField, Header("カメラ調整")]
    float y;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("player");
        shodow = GameObject.Find("shodow");
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 playerSize = player.GetComponent<SpriteRenderer>().bounds.size;

        //カメラの追従
        transform.position = new Vector3(shodow.transform.position.x,
                                         shodow.transform.position.y,
                                         transform.position.z);

        //縦のサイズ
        float px = player.transform.position.x - shodow.transform.position.x;
        float py = player.transform.position.y - shodow.transform.position.y;

        if (Mathf.Abs(py) >= 4.5f)
            Camera.main.orthographicSize = Mathf.Abs(py) + (player.GetComponent<SpriteRenderer>().bounds.size.y / 2);


        //横のサイズ

        //print(Mathf.Abs(px - (playerSize.x / 2)));

        //左
        if (Mathf.Abs(px) >= 8)
            //          Camera.main.orthographicSize = Mathf.Abs(px) + (playerSize.x / 2);
            Camera.main.orthographicSize = Mathf.Abs(px) + (player.GetComponent<SpriteRenderer>().bounds.size.x / 2);

        //if (player.transform.position.x - (playerSize.x / 2) <= CameraTopLeft().x)
        //{
        //    print("aaaa");
        //    Camera.main.orthographicSize += 0.1f;
        //}
        //else
        //{
        //    Camera.main.orthographicSize -= 0.1f;
        //}

        //if (player.transform.position.x + (playerSize.x / 2) >= CameraBottomRight().x)
        //{
        //    print("atari");
        //    Camera.main.orthographicSize -= 0.1f;
        //}


        if (Camera.main.orthographicSize < 8)
            Camera.main.orthographicSize = 5;

        //transform.position = new Vector3
        //    (player.transform.position.x,
        //     player.transform.position.y + y,
        //     transform.position.z
        //     );
    }

    private Vector3 CameraTopLeft()
    {
        //カメラのワールド座標を取得
        Vector3 cameraWorldPosLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);

        return cameraWorldPosLeft;
    }

    private Vector3 CameraBottomRight()
    {
        Vector3 cameraWorldPosRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,0));

        return cameraWorldPosRight;
    }
}
