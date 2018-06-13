using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Show : MonoBehaviour {

    Image mapPanel;

    InputField prefabName;

    Button saveButton;

    Transform[] mapPanelChilden;
    Transform[] prefabNameChilden;
    Transform[] saveButtonChilden;

    bool flgKeyCodeM = true;
    bool flgKeyCodeS = true;

    // Use this for initialization
    void Start () {
        //オブジェクトを取得
        mapPanel = GameObject.Find("MapPanel").GetComponent<Image>();
        prefabName = GameObject.Find("prefabName").GetComponent<InputField>();
        saveButton = GameObject.Find("SaveButton").GetComponent<Button>();

        //取得したオブジェクトの子オブジェクトを全て取得
        mapPanelChilden = mapPanel.transform.GetComponentsInChildren<Transform>();
        prefabNameChilden = prefabName.transform.GetComponentsInChildren<Transform>();
        saveButtonChilden = saveButton.transform.GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update () {

        //マップチップ(UI)の表示、非表示
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (flgKeyCodeM)
            {
                ChildenShow(mapPanelChilden, false);
                flgKeyCodeM = false;
            }
            else
            {
                ChildenShow(mapPanelChilden, true);
                flgKeyCodeM = true;
            }
        }

        //セーブ関係の表示、非表示
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (flgKeyCodeS)
            {
                ChildenShow(prefabNameChilden, false);
                ChildenShow(saveButtonChilden, false);
                flgKeyCodeS = false;
            }
            else
            {
                ChildenShow(prefabNameChilden, true);
                ChildenShow(saveButtonChilden, true);
                flgKeyCodeS = true;

            }
        }
    }

    private void ChildenShow(Transform[] childen,bool show)
    {
        for(int i = 0;i < childen.Length; i++)
        {
            childen[i].gameObject.SetActive(show);
        }
    }
}
