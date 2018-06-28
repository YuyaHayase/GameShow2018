using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// チュートリアルのカメラ
public class hTutrialCamera : MonoBehaviour {

    [SerializeField]
    GameObject player;

	// Use this for initialization
	void Start () {
        if (player == null) player = GameObject.Find("TutrialPlayer");
	}

	// Update is called once per frame
	void Update () {
        transform.position = player.transform.position + new Vector3(0,0,-10);
	}
}
