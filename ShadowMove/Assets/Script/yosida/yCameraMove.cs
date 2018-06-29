using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
class yCameraRange
{
    [SerializeField,Header("ステージの名前")]
    string stageName;
    [SerializeField, Header("カメラが動ける最大値")]
    Vector2 max;
    [SerializeField, Header("カメラが動ける最小値")]
    Vector2 min;

    public string StageName
    {
        get { return stageName; }
    }

    public Vector2 Max
    {
        get { return max; }
    }

    public Vector2 Min
    {
        get { return min; }
    }

}

public class yCameraMove : MonoBehaviour {

    GameObject stage;
    GameObject player;
    GameObject shodow;

    [SerializeField, Header("ステージごとのカメラの範囲")]
    List<yCameraRange> cameraRange;

    int stageNum;

    [SerializeField,Header("カメラのy座標の調整")]
    float y;
    [SerializeField,Header("カメラ移動")]
    float speed = 1.0f;
    
    fShodow _fShodow;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("player");
        shodow = GameObject.Find("shodow");
        _fShodow = shodow.GetComponent<fShodow>();

        StartCoroutine("Delay");
    }
	
	// Update is called once per frame
	void Update () {

        //乗り移っている最中
        if (_fShodow.FlgPossess)
        {
            //カメラを乗り移っているオブジェクトに追従
            if (transform.position == _fShodow.obj.transform.position)
            {
                transform.position = new Vector3(_fShodow.obj.transform.position.x,
                                                 _fShodow.obj.transform.position.y,
                                                 transform.position.z);
            }
            else//カメラをオブジェクトの位置までだんだん移動する
            {
                float x = Mathf.MoveTowards(transform.position.x, _fShodow.obj.transform.position.x, speed);
                float y = Mathf.MoveTowards(transform.position.y, _fShodow.obj.transform.position.y, speed);
                transform.position = new Vector3(x, y, transform.position.z);
            }
        }
        else//乗り移っていない
        {
            //カメラを影に追従
            if (transform.position == shodow.transform.position)
            {
                transform.position = new Vector3(shodow.transform.position.x,
                                                 shodow.transform.position.y,
                                                 transform.position.z);
            }
            else//カメラを影の位置までだんだん移動する
            {
                float x = Mathf.MoveTowards(transform.position.x, shodow.transform.position.x, speed);
                float y = Mathf.MoveTowards(transform.position.y, shodow.transform.position.y, speed);
                transform.position = new Vector3(x, y, transform.position.z);
            }
        }

        if (cameraRange[stageNum].Max != null || cameraRange[stageNum].Min != null)
        {
            Vector3 pos = transform.position;
            if (transform.position.x > cameraRange[stageNum].Max.x)
                pos.x = cameraRange[stageNum].Max.x;
            if (transform.position.x < cameraRange[stageNum].Min.x)
                pos.x = cameraRange[stageNum].Min.x;
            if (transform.position.y > cameraRange[stageNum].Max.y)
                pos.y = cameraRange[stageNum].Max.y;
            if (transform.position.y < cameraRange[stageNum].Min.y)
                pos.y = cameraRange[stageNum].Min.y;
            transform.position = pos;
        }

    }

    private IEnumerator Delay()
    {
        enabled = false;

        yield return new WaitForEndOfFrame();
        stage = GameObject.FindGameObjectWithTag("Stage");
        print(stage);

        for (int i = 0; i < cameraRange.Count; i++)
        {
            if (stage.name == cameraRange[i].StageName + "(Clone)")
            {
                stageNum = i;
                break;
            }
        }

        enabled = true;
        yield break;
    }
}
