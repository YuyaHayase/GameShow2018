using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yShodowManager : MonoBehaviour {

    Vector2 max;
    Vector2 min;

	// Use this for initialization
	void Start () {
	}

    // Update is called once per frame
    void Update()
    {
        max.x = Camera.main.transform.position.x + 8;
        max.y = Camera.main.transform.position.y + 5;
        min.x = Camera.main.transform.position.x - 8;
        min.y = Camera.main.transform.position.y - 5;

        Vector3 pos = transform.position;
        if (transform.position.x > max.x)
            pos.x = max.x;
        if (transform.position.x < min.x)
            pos.x = min.x;
        if (transform.position.y > max.y)
            pos.y = max.y;
        if (transform.position.y < min.y)
            pos.y = min.y;

        transform.position = pos;
    }
}
