using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yFallGameOver : MonoBehaviour {

    yPlayerManager _yPlayerManager;

	// Use this for initialization
	void Start () {
        _yPlayerManager = GameObject.Find("player").GetComponent<yPlayerManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            _yPlayerManager.FlgDeath = true;
        }
    }
}
