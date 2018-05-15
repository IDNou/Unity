using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenTree : MonoBehaviour
{
    public float fallSpeed;
    public float RecoveryTime = 10.0f;

    private void Update()
    {
       //if(this.GetComponentInParent<Status>().nHP < 0)
       // {
       //     if (this.transform.rotation.eulerAngles.x >= 88.5f)
       //     {
       //         StartCoroutine(fallTree());
       //         return;
       //     }
       //     else
       //     {
       //         this.transform.Rotate(fallSpeed * Time.deltaTime, 0, 0);
       //     }
       // }
    }

    IEnumerator fallTree()
    {
        yield return new WaitForSeconds(2.0f);

        this.transform.parent.gameObject.SetActive(false);
    }
}
