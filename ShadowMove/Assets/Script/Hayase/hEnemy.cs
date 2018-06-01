using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class hEnemy : MonoBehaviour {

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

    // 座標記憶
    public Vector2 pos;

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
                CharacterMove(CharacterMoveDirection.Top);
                CharacterMove(CharacterMoveDirection.Right);
                break;
            case CharacterMoveDirection.TopLeft:
                CharacterMove(CharacterMoveDirection.Top);
                CharacterMove(CharacterMoveDirection.Left);
                break;
            case CharacterMoveDirection.BottomRight:
                CharacterMove(CharacterMoveDirection.Bottom);
                CharacterMove(CharacterMoveDirection.Right);
                break;
            case CharacterMoveDirection.BottomLeft:
                CharacterMove(CharacterMoveDirection.Bottom);
                CharacterMove(CharacterMoveDirection.Left);
                break;
        }

        pos = new Vector2(dx / 30.0f, dy / 30.0f);
        return pos;
    }
}
