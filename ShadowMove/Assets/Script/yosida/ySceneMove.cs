using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ySceneMove : MonoBehaviour {

    yFade _yfade;

	// Use this for initialization
	void Start () {
        _yfade = GameObject.Find("fadeImage").GetComponent<yFade>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            _yfade.FlgFadeOut = true;
            coll.GetComponent<yPlayerAI>().Speed = 0;

        }
    }
}
