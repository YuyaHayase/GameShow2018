using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapEditor : MonoBehaviour {

    [SerializeField, Header("SpriteRendererのPrefab")]
    SpriteRenderer sprPrefab;

    [SerializeField, Header("生成されるマップの大きさ")]
    Vector3 mapScale = new Vector3(3.1f, 3.1f, 1);

    MapPanel mapPanel;

	// Use this for initialization
	void Start () {
        mapPanel = GameObject.Find("MapPanel").GetComponent<MapPanel>();
	}
	
	// Update is called once per frame
	void Update () {
        //マウスの位置を取得
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //ワールド座標と接触しているか
        Collider2D collider2D = Physics2D.OverlapPoint(mousePos);

        if (collider2D)
        {
            //UIに当たったらこれ以上の処理はやらない
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            //Rayを飛ばす
            RaycastHit2D[] rayHit2D = Physics2D.RaycastAll(mousePos,Vector2.zero);

            //左クリックで生成
            if (Input.GetMouseButton(0))
            {
                if (rayHit2D.Length < 2)
                {
                    //生成
                    SpriteRenderer spr = Instantiate(sprPrefab);

                    //Spriteを入れる
                    spr.sprite = mapPanel.ChipSpr;

                    //色をいれる
                    spr.color = mapPanel.ChipColor;

                    //大きさの調整
                    spr.transform.localScale = mapScale;

                    //Rayのあたり判定をつける
                    spr.gameObject.AddComponent<BoxCollider2D>();

                    //タグを変える
                    spr.tag = "Create";

                    //名前の変更
                    spr.name = "MapTile";

                    //画像の位置
                    Vector3 mapPos;
                    mapPos.x = rayHit2D[0].transform.position.x;
                    mapPos.y = rayHit2D[0].transform.position.y;
                    mapPos.z = 0;
                    spr.transform.position = mapPos;
                }
                else if(rayHit2D[TagNum(rayHit2D, "Create")].collider.CompareTag("Create"))
                {
                    //すでにマップチップが置いてあった場合、Spriteと色だけを変える
                    SpriteRenderer spr = rayHit2D[TagNum(rayHit2D, "Create")].collider.gameObject.GetComponent<SpriteRenderer>();

                    //Spriteをいれる
                    spr.sprite = mapPanel.ChipSpr;

                    //色を入れる
                    spr.color = mapPanel.ChipColor;
                }
            }

            //右クリックで画像(SpriteRenderer)を消す
            if (Input.GetMouseButton(1))
            {
                if(rayHit2D.Length > 1)
                {
                    Destroy(rayHit2D[TagNum(rayHit2D, "Create")].collider.gameObject);
                }
            }
        }
	}

    //タグを探す
    private int TagNum(RaycastHit2D[] rayHit, string tag)
    {
        for (int i = 0; i < rayHit.Length; i++)
        {
            if (rayHit[i].collider.gameObject.tag == tag)
                return i;
        }
        return 0;
    }

}
