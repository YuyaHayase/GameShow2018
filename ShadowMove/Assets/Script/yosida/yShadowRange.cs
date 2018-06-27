using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yShadowRange : MonoBehaviour {


    bool flgRadius = false; //tureになったらscaleの伸びが止まる
    bool flgRange = false;  //影が動ける最大範囲まで延びたらTrue

    fShodow _fShodow;
    yPlayerAI _yPlayerAI;

	// Use this for initialization
	void Start () {
        //スクリプトを取得
        _fShodow = GameObject.Find("shodow").GetComponent<fShodow>();
        _yPlayerAI = transform.parent.GetComponent<yPlayerAI>();
        _yPlayerAI.enabled = false;


        //遅延コルーチン
        StartCoroutine("Delay");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //遅延処理
    IEnumerator Delay()
    {
        //遅延させる
        yield return new WaitForEndOfFrame();

        //オブジェクトの取得
        GameObject radius = GameObject.Find("radiusPos");
        GameObject player = GameObject.Find("player");

        //影の範囲の大きさの最大値の場所
        Vector3 radiusPos = radius.transform.position;


        //半径rの位置まで移動
        while (true)
        {
            Vector2 dir = player.transform.position - radiusPos;

            if (dir.magnitude >= _fShodow.r)
                break;
            else
                radiusPos += new Vector3(0.1f, 0.1f, 0);
        }
        radius.transform.position = radiusPos;
        yield return new WaitForEndOfFrame();

        flgRange = true;

        //影が動ける範囲を取得してこの画像の大きさを調整する
        while (true)
        {
            transform.localScale += new Vector3(0.01f, 0.01f, 0);

            if (flgRadius)
                break;

            yield return new WaitForEndOfFrame();
        }
        Destroy(radius.gameObject);
        _yPlayerAI.enabled = true;
        GetComponent<CircleCollider2D>().enabled = false;
        yield break;
    }


    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("RadiusPos"))
        {
            //範囲に入ったら
            if (flgRange)
            {
                flgRadius = true;
            }
        }
    }
}
