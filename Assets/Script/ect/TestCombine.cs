using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCombine : MonoBehaviour
{
    private void Awake()
    {
        for (int i = 0; i < this.transform.childCount; i++)
            this.transform.GetChild(i).gameObject.SetActive(false);
    }

    private void Start()
    {
        for (int i = 0; i < this.transform.childCount; i++)
            this.transform.GetChild(i).gameObject.SetActive(true);
    }
}
