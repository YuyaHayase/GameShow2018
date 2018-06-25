using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yCameraMove : MonoBehaviour {

    GameObject player;
    GameObject shodow;

    [SerializeField,Header("カメラのy座標の調整")]
    float y;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("player");
        shodow = GameObject.Find("shodow");
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(shodow.transform.position.x, 
                                         shodow.transform.position.y + y, 
                                         transform.position.z);
	} 
}
