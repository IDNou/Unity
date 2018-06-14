using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour {

    public float MaxHP = 100.0f;
    private float HP;
    public float nHP
    {
        get
        {
            return HP;
        }
        set
        {
            HP = value;
        }

    }
    public float ATK = 20.0f;
    public float DEF = 10.0f;

    private UIProgressBar uiProgressBar;
    private UIProgressBar myProgressBar;

    private void Awake()
    {
        uiProgressBar = Resources.Load<UIProgressBar>("Progress Bar");
        myProgressBar = Instantiate(uiProgressBar, GameObject.Find("ProgressPanel").transform);
        myProgressBar.GetComponent<FollowProgressBar>().target = this.gameObject.GetComponentInChildren<Transform>().Find("ProgressBarPos").gameObject;
        myProgressBar.GetComponent<FollowProgressBar>().FillHP();
    }

    private void OnEnable()
    {
        HP = MaxHP;
        myProgressBar.gameObject.SetActive(true);
    }

    private void Update()
    {
        if(HP <= 0.0f)
        {
            this.gameObject.SetActive(false);
            myProgressBar.gameObject.SetActive(false);
        }
    }
}
