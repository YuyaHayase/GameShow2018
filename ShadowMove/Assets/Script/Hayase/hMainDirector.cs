using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

// メインシーン
public class hMainDirector : MonoBehaviour {

    // どのステージにするか
    [SerializeField]
    static int StageSelect;

    [SerializeField, Header("ステージプレハブ")]
    GameObject[] stageName;

    [SerializeField, Header("Enemyプレハブ")]
    GameObject[] Enemys = new GameObject[2];

    [SerializeField, Header("表示用->ステージセレクト")]
    private int selected = StageSelect;

    [SerializeField, Header("表示用->実際の表示ステージ")]
    GameObject DispStage;

    [SerializeField, Header("ボスステージシーン名")]
    string bossStage;

    // csvからエネミーの座標を取得
    private List<string[]> Position = new List<string[]>();

    // 取得されたエネミーを配列に記憶
    private List<GameObject> ee = new List<GameObject>();

    [SerializeField]
    GameObject BGM;

    // Use this for initialization
    void Start () {
        // csvFile
        var filename = "Enemy";
        var csvFile = Resources.Load("CSV/" + filename) as TextAsset;
        var re = new StringReader(csvFile.text);

        while(re.Peek() > -1)
        {
            var lineData = re.ReadLine();
            var address = lineData.Split(',');
            Position.Add(address);
        }

        if (null == BGM) BGM = GameObject.Find("Sound");
        if (null != BGM)
            BGM.GetComponent<fsoundcontolloer>().select_BGM(1);

        StageSelect = 0;
        CreateGround();
    }

    // Update is called once per frame
    void Update () {
        // デバック用 -> 次のステージに変えます
        if (Input.GetKey(KeyCode.LeftShift) & Input.GetKeyDown(KeyCode.P))
            EndedStage();
    }

    // stringデータをVector3にします
    private Vector3 StringToVector3(string a, string b)
    {
        return new Vector3(float.Parse(a), float.Parse(b));
    }

    // ステージにエネミーを置きます
    private void putEnemy()
    {
        foreach (GameObject g in ee)
            Destroy(g);

        foreach (string[] data in Position)
        {
            if (int.Parse(data[0]) - 1 == StageSelect)
            {
                ee.Add(Instantiate(Enemys[int.Parse(data[3])], StringToVector3(data[1].ToString(), data[2].ToString()), Quaternion.identity));
            }
        }
    }

    // 地面の生成
    void CreateGround()
    {
        DispStage = Instantiate(stageName[StageSelect]) as GameObject;
        putEnemy();
    }

    // 地面の削除
    void DeleteGround()
    {
        if (selected + 1 > stageName.Length - 1)
        {
            SceneManager.LoadScene(bossStage);
        }

        Destroy(DispStage);
        StageSelect++;
        selected = StageSelect;
    }

    // ステージが終了したら地面の生成と削除
    public void EndedStage()
    {
        DeleteGround();
        CreateGround();
    }
}
