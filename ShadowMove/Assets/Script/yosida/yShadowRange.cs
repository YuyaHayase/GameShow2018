using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yShadowRange : MonoBehaviour {

    Vector3 scaleSize;

    fShodow _fShodow;

	// Use this for initialization
	void Start () {
        //スクリプトを取得
        _fShodow = GameObject.Find("shodow").GetComponent<fShodow>();

        scaleSize = transform.localScale;

        //遅延コルーチン
        StartCoroutine("Delay");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //遅延処理(取得したいものがStartで設定しているから取れない可能性があるため)
    IEnumerator Delay()
    {
        //1フレーム遅延させる
        yield return new WaitForEndOfFrame();

        //影が動ける範囲を取得してこの画像の大きさを調整する
        transform.localScale = scaleSize* _fShodow.r;

    }
}
