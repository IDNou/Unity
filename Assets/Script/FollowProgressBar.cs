using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowProgressBar : MonoBehaviour
{
    public GameObject target;

    private Camera targetCamera;
    private Camera uiCamera;

    private UIProgressBar uiBar;
    private float maxHP;
    private float currHP;

    private Vector3 ProgressBarPos;

    private void Awake()
    {
        uiBar = this.GetComponent<UIProgressBar>();
        uiBar.foregroundWidget.color = Color.green;

        targetCamera = Camera.main;
        uiCamera = this.GetComponentInParent<Camera>();
    }

    private void Update()
    {
        Vector3 screenPos = targetCamera.WorldToScreenPoint(target.transform.position);

        if(screenPos.z >= 0.0f)
        {
            ProgressBarPos = uiCamera.ScreenToWorldPoint(screenPos);

            ProgressBarPos.x -= 0.15f;
            ProgressBarPos.y += 0.05f;
            ProgressBarPos.z = 0.0f;
            this.transform.position = ProgressBarPos;
        }

        currHP = target.GetComponentInParent<Status>().HP;

        uiBar.value = currHP / maxHP;
    }

    public void FillHP()
    {
        if (target)
        {
            currHP = target.GetComponentInParent<Status>().HP;
            maxHP = target.GetComponentInParent<Status>().MAXHP;
        }
    }
}