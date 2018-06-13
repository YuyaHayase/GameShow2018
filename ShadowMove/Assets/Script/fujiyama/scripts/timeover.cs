using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timeover : MonoBehaviour {
    [SerializeField, Header("RGB"), Range(0, 1.0f)]
    public float red, green, blue,aphle;

    [SerializeField, Header("RGB"), Range(0, 1.0f)]
    float t = 0.0005f; //変化割合

    

    

    int RA = 1, RB = 0, RG = 1;

    int ra = 1, rb = 1, rg = 0;

    int mode = 1;

    [SerializeField]
    Color morining;
    [SerializeField]
    Color aftenoon;
    [SerializeField]
    Color evening;
    [SerializeField]
    Color night;
	// Use this for initialization
	void Start ()
    {
        red = GetComponent<Image>().color.r;
        green = GetComponent<Image>().color.g;
        blue = GetComponent<Image>().color.b;
       
	}
	
	// Update is called once per frame
	void Update ()
    {
        //red = (1.0f - t) * RA + t * ra;
        //green = (1.0f - t) * RG + t * rg;
        //blue = (1.0f - t) * RB +t * rb;

        t += Time.deltaTime * 0.1f;

      switch(mode)
        {
            case 1://朝から昼
                red = (1.0f - t) * morining.r + t * aftenoon.r;
                green = (1.0f - t) * morining.g + t * aftenoon.g;
                blue = (1.0f - t) * morining.b + t * aftenoon.b;
                if (t >= 1)
                {
                    mode++;
                    t = 0;
                }
                break;
            case 2: //昼から夕方
                red = (1.0f - t)   *aftenoon.r + t * evening.r;
                green = (1.0f - t) * aftenoon.g + t * evening.g;
                blue = (1.0f - t)  * aftenoon.b + t * evening.b;
                if (t >= 1)
                {
                    mode++;
                    t = 0;
                }
                break;
            case 3: //夕方から夜
                red = (1.0f - t) * evening.r + t * night.r;
                green = (1.0f - t) * evening.g + t * night.g;
                blue = (1.0f - t) * evening.b + t * night.b;
                break;




        }

        GetComponent<Image>().color = new Color(red, green, blue,1);
	}
}
