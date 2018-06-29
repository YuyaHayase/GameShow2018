using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yCameraEnding : MonoBehaviour {

    GameObject player;

    [SerializeField]
    float y = 2;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("player");
	}
	
	// Update is called once per frame
	void Update () {
        //エンディング用
            transform.position = new Vector3(player.transform.position.x,
                                             player.transform.position.y + y,
                                             transform.position.z);
    }
}
