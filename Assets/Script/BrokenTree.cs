using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenTree : MonoBehaviour
{
    public float fallSpeed;


    private void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.transform.rotation.eulerAngles.x >= 88.5f)
        {
            StartCoroutine(fallTree());
            return;
        }
        else
        {
            this.transform.Rotate(fallSpeed * Time.deltaTime, 0, 0);
        }
    }

    IEnumerator fallTree()
    {
        yield return new WaitForSeconds(2.0f);

        this.transform.parent.gameObject.SetActive(false);
    }
}
