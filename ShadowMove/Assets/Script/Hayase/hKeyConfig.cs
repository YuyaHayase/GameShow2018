using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hKeyConfig : MonoBehaviour {

    // キーコンフィグの設定情報
    public static Dictionary<string, string> Config
        = new Dictionary<string, string>();

    // キー状態取得
    public static bool GetKey(string key)
    {
        return Input.GetKey(Config[key]);
    }

    public static bool GetKeyDown(string key)
    {
        return Input.GetKeyDown(Config[key]);
    }

    public static bool GetKeyUp(string key)
    {
        return Input.GetKeyUp(Config[key]);
    }
}
