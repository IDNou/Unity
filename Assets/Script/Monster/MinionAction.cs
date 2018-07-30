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
            SoundManager.Instance.EFXPlaySound("MetalHeavyBashFlesh" + Random.Range(1,4));
            Status enermyStatus = Enermy.GetComponent<Status>();
            Status myStatus = this.GetComponentInParent<Status>();
            enermyStatus.Marker = this.gameObject;
            enermyStatus.HP -= (int)(myStatus.ATK - (myStatus.ATK * enermyStatus.DEF / (enermyStatus.DEF + 100)));
        }
    }
}
