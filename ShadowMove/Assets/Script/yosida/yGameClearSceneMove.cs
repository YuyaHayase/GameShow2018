using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yGameClearSceneMove : MonoBehaviour {

    yFade _yFade;

	// Use this for initialization
	void Start () {
        _yFade = GameObject.Find("fadeImage").GetComponent<yFade>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.CompareTag("Player"))
        {
            _yFade.FlgFadeOut = true;
            _yFade.NextScene = "GameClear";
            coll.GetComponent<yPlayerAI>().Speed = 0;
        }
    }
}
