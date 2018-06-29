using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fhp : MonoBehaviour {
    public Slider sllidre;

    fShodow fs;
	// Use this for initialization
	void Start ()
    {
        fs = GameObject.Find("shodow").GetComponent<fShodow>();
        sllidre.maxValue = fs.HP;
	}
	
	// Update is called once per frame
	void Update ()
    {
        sllidre.value = 1;
	}
}
