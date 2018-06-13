using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class hEnemy1 : hEnemy {

    [SerializeField, Header("PlayerObject")]
    GameObject player;

    // 攻撃しているかどうか
    static bool isAttacking = false;

    // 移動速度
    [SerializeField, Header("移動速度")]
    float MoveSpeed = 1.0f;

    // 移動方向
    [SerializeField, Header("移動方向")]
    CharacterMoveDirection CharMoveDirection = CharacterMoveDirection.Left;

    // Use this for initialization
	void Start () {
        pos = new Vector2(transform.position.x, transform.position.y);
        if (player == null) Debug.LogError("Playerを入れてください");
	}
	
    // Update
    private void FixedUpdate()
    {
        CharacterStatus(Status.Move);
    }

    // キャラクターのステータス
    public override void CharacterStatus(Status status)
    {
        switch (status)
        {
            case Status.Attack:
                setAttacking = true;
                break;
            case Status.Move:
                pos += CharacterMove(CharMoveDirection) * MoveSpeed;
                transform.position = pos;
                break;
            case Status.Wait:
                break;
        }
    }

    private static bool setAttacking
    {
        set
        {
            isAttacking = value;
        }
    }

    public static bool getAttacking
    {
        get
        {
            return isAttacking;
        }
    }
}
