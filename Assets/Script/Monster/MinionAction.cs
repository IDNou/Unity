using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionAction : MonoBehaviour
{

    public void Attack()
    {
        GameObject Enermy = this.GetComponentInParent<MinionContol>().Enermy;

        if (Enermy)
        {
            Enermy.GetComponent<Status>().Marker = this.gameObject;
            Enermy.GetComponent<Status>().nHP -= this.GetComponentInParent<Status>().ATK;
        }
    }
}
