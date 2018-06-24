using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class hEnemy : MonoBehaviour {

    // プレイヤーオブジェクト
    [Header("PlayerObject")]
    public GameObject player;

    // 移動の遅延
    float MoveDelay = 30.0f;

    // 座標記憶
    [Header("エネミーの座標")]
    public Vector2 pos;

    // 移動方向
    [SerializeField, Header("移動方向")]
    public CharacterMoveDirection CharMoveDirection = CharacterMoveDirection.Left;

    // アニメーションの速度
    [Header("アニメーションの速度")]
    public float AnimationSpeed = 1.0f;

    [Header("ステータス")]
    public Status EnemyState = Status.Wait;

    /// <summary>
    /// エネミーのステータス
    /// </summary>
    public enum Status
    {
        Wait,
        Attack,
        Move
    }

    /// <summary>
    /// エネミーの移動方向
    /// </summary>
    public enum CharacterMoveDirection
    {
        Top,
        TopRight,
        Right,
        BottomRight,
        Bottom,
        BottomLeft,
        Left,
        TopLeft
    }

    // 抽象化キャラクターのステータス
    public abstract void CharacterStatus(Status status);
    
    // 移動
    public Vector2 CharacterMove(CharacterMoveDirection cmd)
    {
        float dx = 0, dy = 0;

        switch (cmd)
        {
            case CharacterMoveDirection.Top:
                dy = 1;
                break;
            case CharacterMoveDirection.Right:
                dx = 1;
                break;
            case CharacterMoveDirection.Bottom:
                dy = -1;
                break;
            case CharacterMoveDirection.Left:
                dx = -1;
                break;
            case CharacterMoveDirection.TopRight:
                dy = 1;
                dx = 1;
                break;
            case CharacterMoveDirection.TopLeft:
                dy = 1;
                dx = -1;
                break;
            case CharacterMoveDirection.BottomRight:
                dy = -1;
                dx = 1;
                break;
            case CharacterMoveDirection.BottomLeft:
                dy = -1;
                dx = -1;
                break;
        }

        pos = new Vector2(dx / MoveDelay, dy / MoveDelay);
        return pos;
    }

    public void DeleteEnemy()
    {
        Destroy(gameObject);
    }
}
