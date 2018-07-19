using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenTree : MonoBehaviour
{
    public float fallSpeed;
    public float RecoveryTime = 10.0f;

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

    private void Update()
    {
        if (this.GetComponentInParent<Status>().HP <= 0)
        {
            if (this.transform.Find("Tree").transform.rotation.eulerAngles.x >= 85.0f)
            {
                StartCoroutine(fallTree());
                return;
            }
            else
            {
                if (this.gameObject.isStatic)
                {
                    print(this.gameObject.isStatic);
                    this.gameObject.isStatic = false;
                    for (int i = 0; i < this.transform.childCount; i++)
                        this.transform.GetChild(i).gameObject.isStatic = false;

                    //GameObject.Find("UpdateBake").GetComponent<NavigationBaker>().reBake();
                }
                this.transform.Find("Tree").transform.Rotate(120.0f * Time.deltaTime, 0, 0);
            }
        }
    }

    IEnumerator fallTree()
    {
        yield return new WaitForSeconds(2.0f);

        this.gameObject.SetActive(false);
        GameObject.Find("UpdateBake").GetComponent<NavigationBaker>().reBake();
        // 살아나게 하려면 디스트로이하면안된다
    }
}
