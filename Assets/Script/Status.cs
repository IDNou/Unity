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

    private void OnEnable()
    {
        HP = MaxHP;
    }

    private void Update()
    {
        if(HP <= 0.0f)
        {
           this.gameObject.SetActive(false);
        }
    }
}
