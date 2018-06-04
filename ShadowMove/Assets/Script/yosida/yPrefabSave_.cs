using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;

public class yPrefabSave_ : MonoBehaviour {

    GameObject prefab;
    InputField prefabName;

    [SerializeField, Header("Prefabの保存先")]
    string prefabDir = "Prefab/";

    // Use this for initialization
    void Start () {
        prefab = GameObject.Find("prefab");
        prefabName = GameObject.Find("prefabName").GetComponent<InputField>();
        prefabName.text = "sample";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //マップタイルを取得しPrefabにして保存する(button用)
    public void PrefabSave()
    {
        //SpriteRendererを全て取得し、MapTileという名前をprefabに入れる
        SpriteRenderer[] spr = FindObjectsOfType<SpriteRenderer>();

        for(int i = 0;i < spr.Length; i++)
        {
            if (spr[i].name == "MapTile")
                spr[i].transform.parent = prefab.transform;
        }

        //Prefab保存フォルダパス
        string prefabDirPath = Application.dataPath + "/Resources/" + prefabDir;

        //指定したパスにディレクトリが存在しない場合新しく作る
        if (!Directory.Exists(prefabDirPath))
            Directory.CreateDirectory(prefabDirPath);

        //Prefab保存パス
        string prefabPath = prefabDirPath + prefabName.text + ".prefab";

        //パスがなければ新しく作る
        if (!File.Exists(prefabPath))
            File.Create(prefabPath);

        //Prefabの保存
        PrefabUtility.CreatePrefab("Assets/" + prefabDir + prefabName.text + ".prefab", prefab);
        AssetDatabase.SaveAssets();
    }

}
