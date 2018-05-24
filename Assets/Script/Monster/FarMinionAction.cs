using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarMinionAction : MonoBehaviour
{
    public GameObject AttackParticle;
    public Transform FirePos;

    private void VoidAttack()
    {
        if (this.GetComponentInParent<FarMinionAnim>().Enermy)
        {
            GameObject test = Instantiate(AttackParticle);
            test.transform.position = FirePos.position;
            Vector3 dir = this.GetComponentInParent<FarMinionAnim>().Enermy.transform.position - FirePos.position;
            dir.Normalize();
            test.GetComponent<Attack>().startDirection = dir;
            test.GetComponent<Attack>().startMagnitude = 100.0f;
            test.GetComponent<Attack>().Enermy = this.GetComponentInParent<FarMinionAnim>().Enermy;
            test.GetComponent<Attack>().ATK = GetComponentInParent<Status>().ATK;
        }

        //GetComponentInParent<FarMinionAnim>().Enermy.GetComponent<Status>().HP -= GetComponentInParent<Status>().ATK;
    }

    private void DragonAttack()
    {
        if (this.GetComponentInParent<FarMinionAnim>().Enermy)
        {
            GameObject test = Instantiate(AttackParticle);
            test.transform.position = FirePos.position;
            Vector3 dir = this.GetComponentInParent<FarMinionAnim>().Enermy.transform.position - FirePos.position;
            dir.Normalize();
            test.GetComponent<Attack>().startDirection = dir;
            test.GetComponent<Attack>().startMagnitude = 100.0f;
            test.GetComponent<Attack>().Enermy = this.GetComponentInParent<FarMinionAnim>().Enermy;
            test.GetComponent<Attack>().ATK = GetComponentInParent<Status>().ATK;
        }
        //GetComponentInParent<FarMinionAnim>().Enermy.GetComponent<Status>().HP -= GetComponentInParent<Status>().ATK;
    }
}