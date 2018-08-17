using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowProgressBar : MonoBehaviour
{
    public GameObject target;

    private Camera targetCamera;
    private Camera uiCamera;

    private UIProgressBar HPUIBar;
    private UIProgressBar MPUIBar;
    private float maxHP;
    private float currHP;
    private float MaxMP;
    private float currMP;

    private Vector3 ProgressBarPos;

    private void Awake()
    {
        
        HPUIBar = this.transform.Find("HPProgress Bar").GetComponent<UIProgressBar>();
        HPUIBar.foregroundWidget.color = Color.green;
        MPUIBar = this.transform.Find("MPProgress Bar").GetComponent<UIProgressBar>();
        MPUIBar.foregroundWidget.color = Color.blue;

        targetCamera = Camera.main;
        uiCamera = this.GetComponentInParent<Camera>();
    }

    private void Update()
    {
        Vector3 screenPos = targetCamera.WorldToScreenPoint(target.transform.position);
        
        ProgressBarPos = uiCamera.ScreenToWorldPoint(screenPos);

        ProgressBarPos.x -= 0.15f;
        ProgressBarPos.y += 0.05f;
        ProgressBarPos.z = 0.0f;
        this.transform.position = ProgressBarPos;

        currHP = target.GetComponentInParent<Status>().HP;
        maxHP = target.GetComponentInParent<Status>().MAXHP;
        currMP = target.GetComponentInParent<Status>().MP;
        MaxMP = target.GetComponentInParent<Status>().MAXMP;

        HPUIBar.value = currHP / maxHP;
        MPUIBar.value = currMP / MaxMP;
    }

    public void FillHP()
    {
        if (target)
        {
            currHP = target.GetComponentInParent<Status>().HP;
            maxHP = target.GetComponentInParent<Status>().MAXHP;
            MaxMP = target.GetComponentInParent<Status>().MAXMP;
            currMP = target.GetComponentInParent<Status>().MP;
        }
    }
}