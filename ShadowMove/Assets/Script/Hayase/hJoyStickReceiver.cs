using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hJoyStickReceiver : MonoBehaviour {

    [SerializeField, Tooltip("デバック")]
    private bool IsDebug = true;

    [SerializeField, Tooltip("画面表示")]
    Text KeyDisp;

    // ボタン列挙
    public enum PlayStationContoller
    {
        Square,
        Cross,
        Circle,
        Triangle,
        L1,
        R1,
        L2,
        R2,
        Share,
        Option,
        L3,
        R3,
        PSButton,
        TrackPad,
        AnotherButton
    }

    // ボタンの番号取得
    public string GetPlayBtn(PlayStationContoller s)
    {
        string jbc = "0";
        switch (s)
        {
            case PlayStationContoller.Square:
                jbc = "0";
                break;
            case PlayStationContoller.Cross:
                jbc = "1";
                break;
            case PlayStationContoller.Circle:
                jbc = "2";
                break;
            case PlayStationContoller.Triangle:
                jbc = "3";
                break;
            case PlayStationContoller.L1:
                jbc = "4";
                break;
            case PlayStationContoller.R1:
                jbc = "5";
                break;
            case PlayStationContoller.L2:
                jbc = "6";
                break;
            case PlayStationContoller.R2:
                jbc = "7";
                break;
            case PlayStationContoller.Share:
                jbc = "8";
                break;
            case PlayStationContoller.Option:
                jbc = "9";
                break;
            case PlayStationContoller.L3:
                jbc = "10";
                break;
            case PlayStationContoller.R3:
                jbc = "11";
                break;
            case PlayStationContoller.PSButton:
                jbc = "12";
                break;
            case PlayStationContoller.TrackPad:
                jbc = "13";
                break;
        }
        return "joystick button " + jbc;
    }

    // start
    void Start()
    {
        /*
        GameObject g = transform.FindChild("humer").gameObject;

        Renderer r = g.GetComponent<Renderer>();
        r.material.EnableKeyword("_EMISSION");
        r.material.SetColor("_EmissionColor", new Color(1,0.5f,0));
        */
    }

    // update
    void Update()
    {
        if (IsDebug && Input.anyKeyDown)
        {
            KeyDisp.text = DisplayButtonName() + "\n" + KeyDisp.text;
            DisplayButtonName();
        }
    }

    // ボタンが押された時
    public string DisplayButtonName()
    {
        return ControlButtonKeys() + " " + ControlButtonKeys().ToString();
    }

    public string ControlButtonKeys()
    {
        PlayStationContoller ps = PlayStationContoller.AnotherButton;

        // □ボタン
        if (Input.GetKey(GetPlayBtn(PlayStationContoller.Square))) ps = PlayStationContoller.Square;

        // ×
        if (Input.GetKey(GetPlayBtn(PlayStationContoller.Cross))) ps = PlayStationContoller.Cross;

        // ◯
        if (Input.GetKey(GetPlayBtn(PlayStationContoller.Circle))) ps = PlayStationContoller.Circle;

        // △
        if (Input.GetKey(GetPlayBtn(PlayStationContoller.Triangle))) ps = PlayStationContoller.Triangle;

        // L1
        if (Input.GetKey(GetPlayBtn(PlayStationContoller.L1))) ps = PlayStationContoller.L1;

        // R1
        if (Input.GetKey(GetPlayBtn(PlayStationContoller.R1))) ps = PlayStationContoller.R1;

        // L2
        if (Input.GetKey(GetPlayBtn(PlayStationContoller.L2))) ps = PlayStationContoller.L2;

        // R2
        if (Input.GetKey(GetPlayBtn(PlayStationContoller.R2))) ps = PlayStationContoller.R2;

        // L3
        if (Input.GetKey(GetPlayBtn(PlayStationContoller.L3))) ps = PlayStationContoller.L3;

        // R3
        if (Input.GetKey(GetPlayBtn(PlayStationContoller.R3))) ps = PlayStationContoller.R3;

        // Share
        if (Input.GetKey(GetPlayBtn(PlayStationContoller.Share))) ps = PlayStationContoller.Share;

        // Option
        if (Input.GetKey(GetPlayBtn(PlayStationContoller.Option))) ps = PlayStationContoller.Option;

        // PSButton
        if (Input.GetKey(GetPlayBtn(PlayStationContoller.PSButton))) ps = PlayStationContoller.PSButton;

        // TrackPad
        if (Input.GetKey(GetPlayBtn(PlayStationContoller.TrackPad))) ps = PlayStationContoller.TrackPad;

        return GetPlayBtn(ps);
    }
}
