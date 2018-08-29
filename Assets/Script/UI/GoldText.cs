using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldText : MonoBehaviour
{
    public GameObject target;

    private Camera targetCamera;
    private Camera uiCamera;

    private Vector3 GoldTextPos;
    //private Vector3 screenPos;

    private void Start()
    {
        targetCamera = Camera.main;
        uiCamera = this.GetComponentInParent<Camera>();
        //screenPos = targetCamera.WorldToScreenPoint(target.transform.position);
    }

    private void Update()
    {
        Vector3 screenPos = targetCamera.WorldToScreenPoint(target.transform.position);

        if (screenPos.z >= 0.0f)
        {
            GoldTextPos = uiCamera.ScreenToWorldPoint(screenPos);
            this.transform.position = GoldTextPos;
        }

        if (this.GetComponent<UILabel>().alpha == 0)
            Destroy(this.gameObject);
    }
}
