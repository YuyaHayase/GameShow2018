using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yOutAttack : MonoBehaviour {

    GameObject player;

    Vector3 outPos;     //最終的にたどり着く場所
    Vector3 playerPos;  //Playerの位置
    Vector3 middlePos;  //Playerと自身の中間地点の位置
    Vector3 startPos;   //自身の最初の位置

    [SerializeField,Header("Playerに移動するまでの速度の減速率")]
    float speedAccele = 1.0f;
    float t = 0;

    // Use this for initialization
    void Start () {
        //オブジェクトの取得
        player = GameObject.Find("player");

        //位置の取得
        playerPos = player.transform.position;
        startPos = transform.position;
        middlePos = (player.transform.position - transform.position) / 2;

        middlePos.y += 3.0f;
	}

    // Update is called once per frame
    void Update()
    {
        //ベジエ曲線
        t += Time.deltaTime * speedAccele;

        outPos.x = (1 - t) * (1 - t) * startPos.x + 2 * (1 - t) * t * middlePos.x + t * t * playerPos.x;
        outPos.y = (1 - t) * (1 - t) * startPos.y + 2 * (1 - t) * t * middlePos.y + t * t * playerPos.y;

        transform.position = outPos;

        //終わったら削除
        if (t >= 1)
            Destroy(gameObject);
    }
}
