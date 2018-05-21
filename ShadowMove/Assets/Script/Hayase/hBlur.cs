using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class hBlur : MonoBehaviour {

    // ブラーの係数
    private int BlurCount = 0;

    // ブラーの上限、下限
    int BlurCeling = 3, BlurFloor = 0;

    // ブラースクリプト
    [SerializeField, Header("BlurOptimised")]
    BlurOptimized BlurOptim;

    // 加算か減算
    public enum PlusMinus
    {
        plus = 1,
        minus = -1
    }

    // デバック
    [SerializeField, Tooltip("デバック")]
    bool debug;

	// Use this for initialization
	void Start () {
        // もしアタッチしてなかったら、代入
        if (null == BlurOptim) BlurOptim = GetComponent<BlurOptimized>();
	}
	
	// Update is called once per frame
	void Update () {
        if (debug)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                BlurChange(PlusMinus.plus);
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                BlurChange(PlusMinus.minus);
            }
            if (Input.GetKeyDown(KeyCode.E)) BlurSwitch();
        }
    }

    // ブラーエフェクトを設定
    public void BlurSwitch(bool enable)
    {
        BlurOptim.enabled = enable; ;
    }

    // ブラーエフェクトON・OFFを切り替える
    public void BlurSwitch()
    {
        BlurOptim.enabled = !BlurOptim.enabled;
    }

    // 実設定
    /// <summary>
    /// ブラーを変えるよ
    /// </summary>
    /// <param name="ps">加算か減算か</param>
    public void BlurChange(PlusMinus ps)
    {
        int Math = (ps == PlusMinus.plus) ? 1 : -1;

        BlurCount += 1 * Math;

        if (BlurFloor > BlurCount || BlurCount > BlurCeling)
        {
            if (BlurFloor > BlurCount - 1) BlurCount += 1;
            if (BlurCeling < BlurCount + 1) BlurCount -= 1;
        }
        BlurOptim.downsample = BlurCount;
        BlurOptim.blurIterations = BlurCount + 1;
    }
}
