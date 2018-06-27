using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yAnimation : MonoBehaviour {

    SpriteRenderer mySpr;

    [SerializeField, Header("アニメーションしたい画像")]
    Sprite[] spr;


    [SerializeField,Header("フレーム")]
    float frame;

    [SerializeField]
    bool flgAnimation = true;

    public bool FlgAnimation
    {
        get { return flgAnimation; }
        set { flgAnimation = value; }
    }

	// Use this for initialization
	void Start () {
        mySpr = GetComponent<SpriteRenderer>();
        StartCoroutine("Animation");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator Animation()
    {
        //無限ループ
        while (true)
        {
            for(int i = 0;i < spr.Length; i++)
            {
                //画像の入れかえ
                mySpr.sprite = spr[i];

                //フレーム数待機
                for (int j = 0; j < frame; j++)
                    yield return new WaitForEndOfFrame();

                //trueなら問題なし
                yield return new WaitUntil(() => flgAnimation);
            }

            for (int i = spr.Length - 2;i > 0; i--)
            {
                //画像の入れ替え
                mySpr.sprite = spr[i];

                //フレーム数待機
                for (int j = 0; j < frame; j++)
                    yield return new WaitForEndOfFrame();

                //trueなら問題なし
                yield return new WaitUntil(() => flgAnimation);
            }
        }
    }
}
