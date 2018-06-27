using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fgameover_camera : MonoBehaviour {

    [SerializeField, Header("背景色")]
    Color color1, color2, color3,color4;

    [SerializeField, Header("変化量")]
    public float plus = 0;

    float t; //変化割合

    public float red, green, blue;

    public int mode = 1;　  //線形補完の順番を保存しておくもの

    

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        t += plus*Time.deltaTime; //変化量を加算

        switch (mode)
        {
            case 1://朝から昼
                red = (1.0f - t) * color1.r + t * color2.r;
                green = (1.0f - t) * color1.g + t * color2.g;
                blue = (1.0f - t) * color1.b + t * color2.b;
                if (t >= 1)
                {
                    mode++;
                    t = 0;
                }
                break;
            case 2: //昼から夕方
                red = (1.0f - t) * color2.r + t * color3.r;
                green = (1.0f - t) * color2.g + t *color3.g;
                blue = (1.0f - t) * color2.b + t * color3.b;
                if (t >= 1)
                {
                    mode++;
                    t = 0;
                }
                break;
            case 3: //夕方から夜
                red = (1.0f - t) * color3.r + t * color4.r;
                green = (1.0f - t) * color3.g + t * color4.g;
                blue = (1.0f - t) * color3.b + t * color4.b;
                break;
        }

        Camera.main.backgroundColor = new Color(red, green, blue, 1);
    }
}
