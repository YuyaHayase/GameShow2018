using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hTutrialDirector : MonoBehaviour {

    [SerializeField, Multiline(5)]
    string _txt = "プレイヤーを止める場所です";


    [SerializeField, Header("マップタイル")]
    GameObject[] Obj = new GameObject[5];

    [SerializeField]
    float PlayerSpeed = 0.1f;


	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Obj[0].tag = "block";
        }
	}
}
