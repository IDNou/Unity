using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillMode : MonoBehaviour
{
    public float coolTime;
    public bool EnableSkill;

    private bool SetSkill;
    private UISprite uiSp;
    private GameObject Player;

    private void Awake()
    {
        uiSp = this.GetComponent<UISprite>();
        uiSp.type = UIBasicSprite.Type.Filled;

        uiSp.fillAmount = 0.0f;
        EnableSkill = false;
        SetSkill = false;
        uiSp.invert = true;

        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (EnableSkill)
            {
                EnableSkill = false;
                SetSkill = false;
                uiSp.fillAmount = 0.0f;
            }
            else
            {
                EnableSkill = true;
                uiSp.fillAmount = 1.0f;
            }
        }

        if (EnableSkill)
        {
            if(!SetSkill)
            {
                SetSkill = true;
                uiSp.fillAmount = 1.0f;
            }

            if (uiSp.fillAmount < 1.0f)
                uiSp.fillAmount += Time.deltaTime / coolTime; // 10초 쿨
        }
        else
        {
            SetSkill = false;
            uiSp.fillAmount = 0.0f;
        }
    }

    public bool AbleSkil()
    {
        bool AbleSkill = false;
        if (uiSp.fillAmount == 1.0f)
            AbleSkill = true;

        return AbleSkill;
    }

    public void SetCoolTime()
    {
        uiSp.fillAmount = 0.0f;
    }

    //public void SetEnableSkill()
    //{
    //    EnableSkill = true;
    //    uiSp.fillAmount = 1.0f;
    //}
}