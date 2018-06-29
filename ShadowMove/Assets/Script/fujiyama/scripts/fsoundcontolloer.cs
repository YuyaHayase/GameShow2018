using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fsoundcontolloer : MonoBehaviour {

    [SerializeField, Header("シングルトン用のフラグ")]
    static bool flg=true;

    [SerializeField,Header("BGMを入れてね")]
    public AudioClip[] BGM;


    private AudioSource audio;

    [SerializeField,Header("SEをいれてね")]
    public AudioClip[] SE;




    // Use this for initialization
    void Start ()
    {
		if(flg==true)
        {
            DontDestroyOnLoad(this);
            flg = false;
        }
        else
        {
            Destroy(gameObject);
        }

	}
	
	// Update is called once per frame


    public void select_BGM(int i = 0)
    {
        audio.clip = BGM[i];
        audio.Play();
    }

    public void select_SE(int j = 0)
    {
        audio.PlayOneShot(SE[j]);
    }
}
