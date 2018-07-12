using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProgressBar : MonoBehaviour
{
    public GameObject target;

    private UIProgressBar HPUIBar;
    private UIProgressBar MPUIBar;
    private UIProgressBar EXPUIBar;

    private float maxHP;
    private float currHP;
    private float maxMP;
    private float currMP;
    private float maxEXP;
    private float currEXP;

    private void Start()
    {
        target = GameObject.Find("Prod");
        HPUIBar = this.transform.Find("HPBar").GetComponent<UIProgressBar>();
        MPUIBar = this.transform.Find("MPBar").GetComponent<UIProgressBar>();
        EXPUIBar = this.transform.Find("ExpBar").GetComponent<UIProgressBar>();
        HPUIBar.GetComponentInChildren<UILabel>().text = maxHP + "/" + maxHP;
        MPUIBar.GetComponentInChildren<UILabel>().text = maxMP + "/" + maxMP;
        EXPUIBar.GetComponentInChildren<UILabel>().text = "0/" + maxEXP;
    }

    private void Update()
    {
        if (target)
        {
            currHP = target.GetComponentInParent<Status>().HP;
            currMP = target.GetComponentInParent<Status>().MP;
            currEXP = target.GetComponentInParent<Status>().CUREXP;
            CheckMaxStatus();

            HPUIBar.GetComponentInChildren<UILabel>().text = (int)currHP + "/" + (int)maxHP;
            MPUIBar.GetComponentInChildren<UILabel>().text = (int)currMP + "/" + (int)maxMP;
            EXPUIBar.GetComponentInChildren<UILabel>().text = (int)currEXP + "/" + (int)maxEXP;

            HPUIBar.value = currHP / maxHP;
            MPUIBar.value = currMP / maxMP;
            EXPUIBar.value = currEXP / maxEXP;
        }
    }

    public void CheckMaxStatus()
    {
        maxHP = target.GetComponentInParent<Status>().MAXHP;
        maxMP = target.GetComponentInParent<Status>().MAXMP;
        maxEXP = target.GetComponentInParent<Status>().MAXEXP;
    }
}
