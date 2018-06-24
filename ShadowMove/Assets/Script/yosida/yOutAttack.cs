using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yOutAttack : MonoBehaviour {

    GameObject player;
    Vector3 outPos;
    Vector3 playerPos;

    float n = 0;
    [SerializeField,Header("Playerに移動するまでの速度")]
    float speedAccele = 1.0f;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("player");
        playerPos = player.transform.position;
	}

    // Update is called once per frame
    void Update()
    {
        //線形補完
        n += Time.deltaTime * speedAccele;

        outPos.x = (1.0f - n) * transform.position.x + n * playerPos.x;
        outPos.y = (1.0f - n) * transform.position.y + n * playerPos.y;

        transform.position = outPos;

        if (n >= 1)
            Destroy(gameObject);
    }
}
