using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hTutrialNeedle : hTitleNeedle {

    // 文字オブジェクトの変数
    [SerializeField, Header("文字")]
    GameObject Yes;
    [SerializeField]
    GameObject No;
    [SerializeField, Header("選択時の色")]
    Color SelectedColor;
    [SerializeField]
    float alpha = 1;
    bool ShadowPut = false;
    GameObject TextShadow;

    private void Start()
    {
        SelectedColor = base.IntToNorm(255, 255, 0);
        t = 0;
        add = 1;
    }

    // Update is called once per frame
    void Update()
    {
        // 目標の変更
        if (Input.GetKeyDown(KeyCode.DownArrow)) next += add;
        if (Input.GetKeyDown(KeyCode.UpArrow)) next -= add;
        if (Input.anyKeyDown)
        {
            ShadowPut = false;
            Destroy(TextShadow);
        }

        // 範囲を超えないようにする
        t = Mathf.Clamp(t, 0, 1f);
        next = Mathf.Clamp(next, 0, 1f);

        // 矢印の選択
        if (Input.GetKeyUp(KeyCode.Return))
        {
            if (t >= 0.5f) base.SceneLoad(SceneName[1]);
            else base.SceneLoad(SceneName[0]);
        }

        // アニメーション
        if (next != t) t += (next - t) * 0.1f;
        if (t < 0.025f) t = 0;
        if (t > 0.975f) t = 1;

        if (!ShadowPut)
        {
            ShadowPut = true;
            if (next > 0.8f) CreateShadow(No);
            if (next < 0.2f) CreateShadow(Yes);
        }

        // 文字の透過
        Rend(Yes, 1 - t / 2.0f);
        Rend(No, Mathf.Abs(t + 0.5f));
    }
    
    protected override void Rend(GameObject g, float t)
    {
        base.Rend(g, t);
        SetAlpha = t;
        if (t > 0.6f) g.GetComponent<Renderer>().material.color = RendColor;
        else g.GetComponent<Renderer>().material.color = new Color(1, 1, 1, alpha);
    }

    private void CreateShadow(GameObject g)
    {
        TextShadow = Instantiate(g) as GameObject;
        TextShadow.transform.position = g.transform.position + new Vector3(0.1f, -0.2f);
        Renderer r = TextShadow.GetComponent<Renderer>();
        r.material.color = base.IntToNorm(100, 180, 180);
        r.sortingOrder = -5;
    }

    // オブジェクトの透明度を変えます
    private Color RendColor
    {
        get
        {
            return new Color(SelectedColor.r, SelectedColor.g, SelectedColor.b, alpha);
        }
    }

    private float SetAlpha
    {
        set
        {
            alpha = value;
        }
    }
}
