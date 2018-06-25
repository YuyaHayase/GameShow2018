using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapPanel : MonoBehaviour {

    GameObject content;

    ScrollRect sr;

    [SerializeField, Header("ボタン")]
    Button prefab; 

    [SerializeField, Header("マップチップの画像を入れる")]
    SpriteRenderer[] mapSprite;

    Vector3 mapPos;

    [SerializeField,Header("マップチップのy軸の間隔")]
    float yInterval = 120.0f;

    Sprite chipSpr;

    Color chipColor;

    public Sprite ChipSpr
    {
        get { return chipSpr; }
    }

    public Color ChipColor
    {
        get { return chipColor; }
    }

	// Use this for initialization
	void Start () {
        content = transform.Find("ScrollPanel").transform.Find("Content").gameObject;
        sr = GetComponent<ScrollRect>();

        //Contentのheightの設定
        if(mapSprite.Length > 3)
        {
            Vector2 contentSizeDelta = content.GetComponent<RectTransform>().sizeDelta;
            for(int i = 3;i< mapSprite.Length; i++)
            {
                contentSizeDelta.y += 150.0f;
            }
            content.GetComponent<RectTransform>().sizeDelta = contentSizeDelta;
        }

        mapPos = prefab.transform.localPosition;

        for (int i = 0;i < mapSprite.Length; i++)
        {
            //ボタンを生成
            Button go = Instantiate(prefab) as Button;

            //Contentの子オブジェクトに
            go.transform.parent = content.transform;

            //大きさを全て１にする
            go.transform.localScale = Vector3.one;
          
            //位置の設定
            go.transform.localPosition = mapPos;

            //Spriteを張る
            go.GetComponent<Image>().sprite = mapSprite[i].sprite;

            //Spriteの色
            go.GetComponent<Image>().color = mapSprite[i].color;

            //一時変数
            Sprite spr = mapSprite[i].sprite;
            Color color = mapSprite[i].color;

            //クリック時の効果をつける
            go.onClick.AddListener(() => MapButton(spr,color));

            //次回の生成位置を変える
            mapPos.y -= yInterval;
        }

        prefab.gameObject.SetActive(false);
        sr.verticalNormalizedPosition = 1;

    }

    // Update is called once per frame
    void Update () {
		
	}

    public void MapButton(Sprite sprite,Color color)
    {
        //自身のSpriteを入れる
        chipSpr = sprite;
        chipColor = color;
    }
}
