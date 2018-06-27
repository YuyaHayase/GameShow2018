using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMove : MonoBehaviour {

    LineRenderer[] gridLine;

    [SerializeField, Header("カメラの動くスピード")]
    float speed = 1.0f;

    GridLines[] grid;

	// Use this for initialization
	void Start () {
        //オブジェクトを取得
        gridLine = Camera.main.GetComponentsInChildren<LineRenderer>();

        //取得したオブジェクトのスクリプトを取得
        grid = new GridLines[gridLine.Length];
        for (int i = 0; i < gridLine.Length; i++)
            grid[i] = gridLine[i].GetComponent<GridLines>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.LeftControl))
        {
            //右に動く
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                Camera.main.transform.position += new Vector3(speed, 0, 0);
                GridMoveX(speed);
            }

            //左に動く
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                Camera.main.transform.position -= new Vector3(speed, 0, 0);
                GridMoveX(-speed);
            }
        }
        else
        {
            //上に動く
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                Camera.main.transform.position += new Vector3(0, speed, 0);
                GridMoveY(speed);
            }

            //下に動く
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                Camera.main.transform.position -= new Vector3(0, speed, 0);
                GridMoveY(-speed);
            }
        }
	}

    //グリッド線のy軸を伸ばす
    void GridMoveY(float yPosMove)
    {
        for(int i = 0;i < gridLine.Length;i++)
        {
            grid[i].yMAX += yPosMove;
            grid[i].yMIN += yPosMove;
        }
    }

    //グリッド線のx軸を伸ばす
    void GridMoveX(float xPosMove)
    {
        for (int i = 0; i < gridLine.Length; i++)
        {
            grid[i].xMAX += xPosMove;
            grid[i].xMIN += xPosMove;
        }
    }

}
