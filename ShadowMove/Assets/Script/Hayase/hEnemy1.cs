using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class hEnemy1 : hEnemy {

    // 攻撃しているかどうか
    static bool isAttacking = false;

    // Use this for initialization
	void Start () {
        pos = new Vector2(transform.position.x, transform.position.y);
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
                pos += CharacterMove(CharacterMoveDirection.Left);
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
