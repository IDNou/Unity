using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour {

    public float MaxHP = 100.0f;
    public float HP = 100.0f;
    public float ATK = 20.0f;
    public float DEF = 10.0f;

    private void OnEnable()
    {
        HP = 100.0f;
    }

    private void Update()
    {
        if(HP <= 0.0f)
        {
            this.gameObject.SetActive(false);
        }
    }
}
