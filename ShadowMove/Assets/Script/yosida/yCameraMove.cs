using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yCameraMove : MonoBehaviour {

    GameObject player;

    [SerializeField,Header("カメラが追従できる最小値")]
    float min;
    [SerializeField,Header("")]
    float max;

    [SerializeField, Header("カメラ調整")]
    float y;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("player");
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = new Vector3
            (player.transform.position.x,
             player.transform.position.y + y,
             transform.position.z
             );
	}
}
