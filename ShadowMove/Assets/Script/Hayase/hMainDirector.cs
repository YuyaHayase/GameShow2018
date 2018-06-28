using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class hMainDirector : MonoBehaviour {

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

    private List<string[]> Position = new List<string[]>();

	// Use this for initialization
	void Start () {
        StageSelect = 1;
        //CreateGround();

        var filename = "Enemy";
        var csvFile = Resources.Load("CSV/" + filename) as TextAsset;
        var re = new StringReader(csvFile.text);

        while(re.Peek() > -1)
        {
            var lineData = re.ReadLine();
            var address = lineData.Split(',');
            Position.Add(address);
        }

        putEnemy();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.P))
            EndedStage();
    }

    // stringデータをVector3にします
    private Vector3 StringToVector3(string a, string b)
    {
        return new Vector3(float.Parse(a), float.Parse(b));
    }

    // ステージにエネミーを起きます
    private void putEnemy()
    {
        foreach (string[] data in Position)
        {
            string aaa = data[0];
            Debug.Log(aaa);
            if (int.Parse(data[0]) - 1 == StageSelect)
            {
                Instantiate(Enemys[int.Parse(data[3])], StringToVector3(data[1].ToString(), data[2].ToString()), Quaternion.identity);
            }
        }
    }

    void CreateGround()
    {
        DispStage = Instantiate(stageName[StageSelect]) as GameObject;
    }

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

    public void EndedStage()
    {
        DeleteGround();
        CreateGround();
    }
}
