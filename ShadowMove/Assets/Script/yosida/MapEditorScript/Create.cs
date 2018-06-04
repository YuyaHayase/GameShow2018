using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create : MonoBehaviour {

    GameObject gridLine;
    BoxCollider2D box2D;

    [SerializeField,Header("グリッド線の最初の位置")]
    Vector3 startPos;
    Vector3 pos;

    float width;//幅
    float height;//高さ
    float interval = 1.0f;

    // Use this for initialization

    private void Awake()
    {
        gridLine = GameObject.Find("GridLine");
        box2D = Camera.main.transform.Find("box").GetComponent<BoxCollider2D>();

        width = box2D.GetComponent<BoxCollider2D>().size.x;
        height = box2D.GetComponent<BoxCollider2D>().size.y;

        pos = startPos;

        //グリッド線を表示
        GridLineX();
        GridLineY();

        BoxInstanse();

    }

    void Start () {
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void GridLineX()
    {
        //x軸のグリッド線を生成
        for (int i = 0; i < 19; i++)
        {
            GameObject go = Instantiate(gridLine);

            GridLines grid = go.GetComponent<GridLines>();

            go.transform.parent = Camera.main.transform;

            go.transform.localPosition = pos;

            go.name = "GridLine";

            grid.GridLineX(pos.x);

            pos.x += interval;
        }
    }
    private void GridLineY()
    {
        //y軸のグリッド線を生成
        for (int i = 0; i < 10; i++)
        {
            GameObject go = Instantiate(gridLine);

            GridLines grid = go.GetComponent<GridLines>();

            go.transform.parent = Camera.main.transform;

            go.transform.localPosition = pos;

            go.name = "GridLine";

            grid.GridLineY(pos.y);

            pos.y -= interval;
        }

    }
    private void BoxInstanse()
    {
        Vector3 boxPos = box2D.transform.position;

        float xStartPos = boxPos.x;
        float xInterval = width;
        float yInterval = height;

        for(int y =0;y < 10; y++)
        {
            boxPos.x = xStartPos;
            for(int x = 0;x < 18; x++)
            {
                BoxCollider2D go = Instantiate(box2D);

                go.transform.parent = Camera.main.transform;

                go.transform.localPosition = boxPos;
                go.tag = "CreatePlace";

                boxPos.x += xInterval;

            }
            boxPos.y -= yInterval;
        }
    }
}
