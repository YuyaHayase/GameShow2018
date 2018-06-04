using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLines : MonoBehaviour {

    LineRenderer grid;

    [SerializeField, Header("線の色")]
    Color color = Color.black;

    [SerializeField,Header("線の太さ")]
    float lineWidth = 0.001f;

    float xMax, xMin, yMax, yMin;

    bool flg = false;

    public float xMAX
    {
        set { xMax = value; }
        get { return xMax; }
    }
    public float xMIN
    {
        set { xMin = value; }
        get { return xMin; }
    }
    public float yMAX
    {
        set { yMax = value; }
        get { return yMax; }
    }
    public float yMIN
    {
        set { yMin = value; }
        get { return yMin; }
    }


    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (flg)
        {
            grid.SetPosition(0, new Vector3(xMax, yMax, 0));

            grid.SetPosition(1, new Vector3(xMin, yMin, 0));
        }
    }

    public void GridLineX(float xPos)
    {
        grid = GetComponent<LineRenderer>();

        //線の色
        grid.startColor = color;
        grid.endColor = color;

        //線の太さ
        grid.startWidth = lineWidth;
        grid.endWidth = lineWidth;


        grid.SetPosition(0, new Vector3(xPos, 5.0f, 0));

        grid.SetPosition(1, new Vector3(xPos, -5.0f, 0));

        xMax = xPos;
        xMin = xPos;
        yMax = 5.0f;
        yMin = -5.0f;
        flg = true;
    }

    public void GridLineY(float yPos)
    {
        grid = GetComponent<LineRenderer>();

        grid.positionCount = 2;

        //線の色
        grid.startColor = color;
        grid.endColor = color;

        //線の太さ
        grid.startWidth = lineWidth;
        grid.endWidth = lineWidth;

        grid.SetPosition(0, new Vector3(9.0f, yPos, 0));

        grid.SetPosition(1, new Vector3(-9.0f, yPos, 0));

        xMax = 9.0f;
        xMin = -9.0f;
        yMax = yPos;
        yMin = yPos;
        flg = true;
    }
}
