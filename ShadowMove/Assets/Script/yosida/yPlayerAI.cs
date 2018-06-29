using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yPlayerAI : MonoBehaviour {

    GameObject rayPointWork;        //次歩く所に歩くかどうか
    GameObject rayPointStepJump;    //次歩く所に段差があるかどうか
    GameObject rayPointHeight;　　　//ジャンプできる高さかどうか
    GameObject rayPointNeedle;      //下に針があるかどうか

    Rigidbody2D rigi2D;

    BoxCollider2D box2D;

    Vector3 startPos;

    [SerializeField, Header("足場が当たっているかどうか")]
    ContactFilter2D filter2D;

    [SerializeField, Header("歩くスピード")]
    float speed;
    float speedSave;

    float workSpeed = 0.0f;

    [Header("----落下する減速率の調整　高いほど落下する速度が速くなる----")]
    [SerializeField, Header("ジャンプの減速率")]
    float jumpAccel = 0.5f;

    [SerializeField, Header("ジャンプしたときの落下の減速率")]
    float jumpFallAccel = 0.3f;

    [SerializeField, Header("自由落下の減速率")]
    float freeFallAccel = 0.1f;

    [Header("----ジャンプ処理---")]
    [SerializeField, Header("初速度")]
    float Vo = 0.3f;

    [SerializeField, Header("時間")]
    float time;

    [SerializeField, Header("重力")]
    float Gravity = 9.8f;

    bool flgJump = false;
    bool flgFilter = false;
    bool flg = true;

    yPlayerManager _yPlayerManger;

    public float Speed
    {
        set { speed = value; }
        get { return speed; }
    }

    public Vector3 StartPos
    {
        get { return startPos; }
    }

    public float SpeedSave
    {
        get { return speedSave; }
    }

    // Use this for initialization
    void Start () {
        startPos = transform.position;
        speedSave = speed;

        //子オブジェクトの取得
        rayPointWork = transform.Find("RayPointWork").gameObject;
        rayPointStepJump = transform.Find("RayPointStepJump").gameObject;
        rayPointHeight = transform.Find("RayPointHeight").gameObject;
        rayPointNeedle = transform.Find("RayPointNeedle").gameObject;

        //コンポーネント取得
        rigi2D = GetComponent<Rigidbody2D>();
        box2D = GetComponent<BoxCollider2D>();
        _yPlayerManger = GetComponent<yPlayerManager>();

    }

    // Update is called once per frame
    void Update () {
        // y = Vot + 1 / 2gt²
        //y = g * t

        //あたり判定
        flgFilter = rigi2D.IsTouching(filter2D);

        //--------------Rayでhitしたオブジェクトを全て取得--------------------------

        //歩けるかどうか
        RaycastHit2D[] hitObjectWork = Physics2D.RaycastAll(rayPointWork.transform.position, Vector2.zero);
        //段差をジャンプ
        RaycastHit2D[] hitObjectStepJump = Physics2D.RaycastAll(rayPointStepJump.transform.position, Vector2.zero);
        //越えられない壁
        RaycastHit2D[] hitObjectHeight = Physics2D.RaycastAll(rayPointHeight.transform.position, Vector2.zero);
        //針あるかどうか
        RaycastHit2D[] hitObjectNeedle = Physics2D.RaycastAll(rayPointNeedle.transform.position, Vector2.zero);

        //print("hitObjectWork = " + hitObjectWork.Length);
        //print("hitObjectStepJump = " + hitObjectStepJump.Length);
        //print("hitObjectHeight = " + hitObjectHeight.Length);
        //print("hitObjectNeedle = " + hitObjectNeedle.Length);

        //Rayで何かを取得したら

        //ジャンプ中は判定しない
        if (!flgJump)
        {
            //歩けるかどうか
            if (FlgHitObject(hitObjectWork))
            {
                //tagがBlockだった場合歩き続ける
                if (FlgTagHitObject(hitObjectWork, "block")
                 || FlgTagHitObject(hitObjectWork, "gimmick"))
                    workSpeed = speed;
                else
                    workSpeed = 0;
            }
            else if (hitObjectWork.Length == 0)
            {
                flgJump = true;
            }

            //針があったら止まる
            if (FlgHitObject(hitObjectNeedle))
            {
                if (FlgTagHitObject(hitObjectNeedle, "Needle"))
                {
                    if (FlgHitObject(hitObjectWork))
                    {
                        if (FlgTagHitObject(hitObjectWork, "block")
                         || FlgTagHitObject(hitObjectWork, "gimmick"))
                        {
                            workSpeed = speed;
                            flgJump = false;
                        }
                    }
                    else
                    {
                        workSpeed = 0;
                        flgJump = false;
                    }
                }
            }


            //ジャンプするかどうか
            if (flgFilter)
            {
                //ジャンプできる高さかどうか
                if (FlgHitObject(hitObjectStepJump) && hitObjectHeight.Length == 0)
                {
                    //一歩先に何かがあったらジャンプ
                    if (FlgTagHitObject(hitObjectStepJump, "block")
                     || FlgTagHitObject(hitObjectStepJump, "Needle")
                     || FlgTagHitObject(hitObjectStepJump, "gimmick"))
                    {
                        flgJump = true;
                        workSpeed = speed;
                    }
                    else
                    {
                        //flgJump = false;
                        //workSpeed = speed;
                    }
                }
                else if (FlgHitObject(hitObjectStepJump) && FlgHitObject(hitObjectHeight))
                {
                    //壁が高かったら
                    flgJump = false;
                    workSpeed = 0;
                    return;
                }
            }
        }

        //ジャンプの挙動
        Jump();


        transform.position += new Vector3(workSpeed, 0, 0);
    }

    //Rayが何かに当たっているかどうか
    private bool FlgHitObject(RaycastHit2D[] hitObject)
    {
        if (hitObject.Length > 0)
            return true;

        return false;
    }

    //タグに当たっているかどうか
    private bool FlgTagHitObject(RaycastHit2D[] hitObject, string tagName)
    {
        for (int i = 0; i < hitObject.Length; i++)
        {
            if (hitObject[i].collider.gameObject.CompareTag(tagName))
            {
                return true;
            }
        }

        return false;
    }

    //ジャンプと自由落下
    private void Jump()
    {
        float y = 0;

        //ジャンプ
        if (flgJump)
        {
            float yMax = y;

            time += Time.deltaTime;
            y = (Vo * time) - Gravity * (time * time) * jumpAccel;

            if (y < yMax)
            {
                y *= jumpFallAccel;
            }

            transform.position += new Vector3(0, y, 0);
        }
        else if (!flgFilter)
        {
            //ジャンプ以外の時の自由落下
            time += Time.deltaTime;
            y = Gravity * time * freeFallAccel;

            transform.position -= new Vector3(0, y, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("block") || coll.gameObject.CompareTag("gimmick"))
        {
            //print("触れている");
            flgJump = false;
            flg = false;
            time = 0;
        }

        if (coll.gameObject.CompareTag("Needle"))
        {
            _yPlayerManger.FlgDeath = true;
            enabled = false;
            //GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("block") || coll.gameObject.CompareTag("gimmick"))
        {
            //print("離れた");

            flg = true;
            time = 0;
        }
    }

}
