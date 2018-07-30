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
            SoundManager.Instance.EFXPlaySound("EtherealMediumHit" + Random.Range(1,4));
            Status myStatus = GetComponentInParent<Status>();
            Status enermyStatus = this.GetComponentInParent<FarMinionAnim>().Enermy.GetComponent<Status>();
            GameObject Attack = Instantiate(AttackParticle);
            Attack.transform.position = FirePos.position;
            Vector3 dir = this.GetComponentInParent<FarMinionAnim>().Enermy.transform.position - FirePos.position;
            dir.Normalize();
            Attack.GetComponent<Attack>().startDirection = dir;
            Attack.GetComponent<Attack>().fSpeed = 5.0f;
            Attack.GetComponent<Attack>().Enermy = this.GetComponentInParent<FarMinionAnim>().Enermy;
            Attack.GetComponent<Attack>().ATK = (int)(myStatus.ATK - (myStatus.ATK * enermyStatus.DEF / (enermyStatus.DEF + 100)));
            Attack.GetComponent<Attack>().Master = this.transform.parent.gameObject;
        }

        //GetComponentInParent<FarMinionAnim>().Enermy.GetComponent<Status>().HP -= GetComponentInParent<Status>().ATK;
    }

    private void DragonAttack()
    {
        if (this.GetComponentInParent<FarMinionAnim>().Enermy)
        {
            SoundManager.Instance.EFXPlaySound("EtherealHeavyHit" + Random.Range(1, 4));
            Status myStatus = GetComponentInParent<Status>();
            Status enermyStatus = this.GetComponentInParent<FarMinionAnim>().Enermy.GetComponent<Status>();
            GameObject Attack = Instantiate(AttackParticle);
            Attack.transform.position = FirePos.position;
            Vector3 dir = this.GetComponentInParent<FarMinionAnim>().Enermy.transform.position - FirePos.position;
            dir.Normalize();
            Attack.GetComponent<Attack>().startDirection = dir;
            Attack.GetComponent<Attack>().fSpeed = 5.0f;
            Attack.GetComponent<Attack>().Enermy = this.GetComponentInParent<FarMinionAnim>().Enermy;
            Attack.GetComponent<Attack>().ATK = (int)(myStatus.ATK - (myStatus.ATK * enermyStatus.DEF / (enermyStatus.DEF + 100)));
            Attack.GetComponent<Attack>().Master = this.transform.parent.gameObject;
        }
        //GetComponentInParent<FarMinionAnim>().Enermy.GetComponent<Status>().HP -= GetComponentInParent<Status>().ATK;
    }
}