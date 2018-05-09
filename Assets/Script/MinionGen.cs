using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionGen : MonoBehaviour
{
    public float GenTime = 0;
    public GameObject Minion;
    public Transform GenPos;

    private void Update()
    {
        if (GenTime <= 0)
        {
            GenTime = 10.0f;
            GameObject GenMinion = Instantiate(Minion, GenPos.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        }
        else
        {
            GenTime -= Time.deltaTime;
        }
    }

}
