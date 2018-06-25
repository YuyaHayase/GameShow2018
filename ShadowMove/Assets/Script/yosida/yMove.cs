using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yMove : MonoBehaviour {

    [SerializeField]
    float speed = 0.1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.RightArrow))
            transform.Translate(speed, 0, 0);

        if (Input.GetKey(KeyCode.LeftArrow))
            transform.Translate(-speed, 0, 0);

        if (Input.GetKey(KeyCode.UpArrow))
            transform.Translate(0, speed, 0);

        if (Input.GetKey(KeyCode.DownArrow))
            transform.Translate(0, -speed, 0);
	}
}
