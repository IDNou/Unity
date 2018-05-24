using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillMode : MonoBehaviour
{
    public float coolTime;

    private UISprite uiSp;
    private GameObject Player;

    private void Awake()
    {
        uiSp = this.GetComponent<UISprite>();
        uiSp.type = UIBasicSprite.Type.Filled;

        uiSp.fillAmount = 1.0f;
        uiSp.invert = true;

        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (uiSp.fillAmount < 1.0f)
            uiSp.fillAmount += Time.deltaTime / coolTime; // 10초 쿨
    }

    public void CoolTime()
    {
        uiSp.fillAmount = 0.0f;
    }
}