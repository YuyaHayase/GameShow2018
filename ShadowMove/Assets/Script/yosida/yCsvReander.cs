using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class yCsvReander : MonoBehaviour {

    [SerializeField,Header("読み込みたいCSVを入れる")]
    TextAsset csv;

    List<string[]> list = new List<string[]>();

    [SerializeField, Header("マップタイルの画像")]
    SpriteRenderer[] mapTile;

    [SerializeField, Header("x軸の始まり")]
    float xStart;
    [SerializeField, Header("y軸の始まり")]
    float yStart;
    [SerializeField, Header("間隔")]
    float interval;

    float xPos, yPos;


    // Use this for initialization
    void Start () {
        StringReader sr = new StringReader(csv.text);
        Reader(sr, list);

        //Listに入っている数字を読み取ってマップを作る
        yPos = yStart;
        for (int y = 0; y < list.Count; y++)
        {
            xPos = xStart;
            for (int x = 0; x < list[y].Length; x++)
            {
                try
                {
                    if (list[y][x] != "")
                    {
                        //マップタイルを生成
                        SpriteRenderer chipSpr = Instantiate(mapTile[int.Parse(list[y][x])], new Vector3(xPos, yPos, 0), Quaternion.identity) as SpriteRenderer;
                        //生成したものに名前を入れる
                        chipSpr.name = "MapTile";
                        //BoxCollider2dを追加
                        chipSpr.gameObject.AddComponent<BoxCollider2D>();
                        //Tagを"block"にする
                        chipSpr.gameObject.tag = "block";
                    }
                }
                catch
                {
                    print("マップ生成エラー");
                }
                xPos += interval;
            }
            yPos -= interval;
        }

    }

    // Update is called once per frame
    void Update () {
		
	}

    //読み込んだものをListに入れていく
    public void Reader(StringReader sr, List<string[]> list)
    {
        while (sr.Peek() != -1)
        {
            string s = sr.ReadLine();
            list.Add(s.Split(','));
        }
    }

}
