using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class yGimmick : MonoBehaviour {

    [SerializeField, Header("マップのデータがあるGameObject")]
    GameObject mapdata;

    [SerializeField, Header("マップタイルの画像")]
    SpriteRenderer[] mapTile;

    [SerializeField, Header("生成位置")]
    Vector3 pos;

    [SerializeField, Header("")]
    int type;

    [SerializeField, Header("生成される間隔")]
    float interval = 1.0f;

    [SerializeField, Header(""), Multiline(20)]
    string mapData;

    List<string[]> list = new List<string[]>();

    yCsvReander _yCsvReander;

	// Use this for initialization
	void Start () {
        _yCsvReander = new yCsvReander();
        StringReader sr = new StringReader(mapData);
        _yCsvReander.Reader(sr, list);

        switch (type)
        {
            case 0://最初右から生成される
                break;
            case 1:
                break;
        }
        StartCoroutine("Gimmick");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Gimmick()
    {
        yield return new WaitForSeconds(5.0f);

        float yPos = pos.y;

        for (int y = list.Count - 1; y >= 0; y--)
        {
            float xPos = pos.x;
            for(int x = 0;x < list[y].Length; x++)
            {
                //ブロックを生成
                SpriteRenderer spr = Instantiate(mapTile[int.Parse(list[y][x])]);

                //生成したオブジェクトをマップデータの子オブジェクトへ
                spr.transform.parent = mapdata.transform;

                //生成位置
                spr.transform.localPosition = new Vector3(xPos, yPos, 0);

                //タグ
                spr.tag = "block";

                //BoxCollider2Dを追加
                spr.gameObject.AddComponent<BoxCollider2D>();

                //次の生成位置をずらす
                xPos += interval;

                //生成する時間を0.1秒遅くする
                yield return new WaitForSeconds(0.1f);
            }
            yPos += interval;
        }
        yield break;
    }
}
