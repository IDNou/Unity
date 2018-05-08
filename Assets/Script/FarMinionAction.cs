using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarMinionAction : MonoBehaviour
{
    public GameObject AttackParticle;

    private void VoidAttack()
    {
        GetComponentInParent<FarMinionAnim>().Enermy.GetComponent<Status>().HP -= GetComponentInParent<Status>().ATK;
    }

    private void DragonAttack()
    {
        GetComponentInParent<FarMinionAnim>().Enermy.GetComponent<Status>().HP -= GetComponentInParent<Status>().ATK;
    }
}