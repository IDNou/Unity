using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AkmaAction : MonoBehaviour
{
    public Transform FirePos;
    public GameObject FireBall;
    public GameObject goImpail;

    private void Awake()
    {

    }

    public void Attack()
    {
        if (this.GetComponentInParent<AkmaControl>().Enermy)
        {
            Status myStatus = GetComponentInParent<Status>();
            Status enermyStatus = this.GetComponentInParent<AkmaControl>().Enermy.GetComponent<Status>();
            GameObject baseAttack = Instantiate(FireBall);
            baseAttack.transform.position = FirePos.position;
            Vector3 dir = this.GetComponentInParent<AkmaControl>().Enermy.transform.position - FirePos.position;
            dir.Normalize();
            baseAttack.GetComponent<Attack>().startDirection = dir;
            baseAttack.GetComponent<Attack>().fSpeed = 5.0f;
            baseAttack.GetComponent<Attack>().ATK = (int)(myStatus.ATK - (myStatus.ATK * enermyStatus.DEF / (enermyStatus.DEF + 100)));
            baseAttack.GetComponent<Attack>().Enermy = this.GetComponentInParent<AkmaControl>().Enermy;
            baseAttack.GetComponent<Attack>().Master = this.transform.parent.gameObject;
            this.GetComponentInParent<AkmaControl>().isAttackCharge = false;
        }
    }

    public void Skill1()
    {
        if (this.GetComponentInParent<AkmaControl>().Enermy)
        {
            GameObject impail = Instantiate(goImpail);
        }
    }

    public void Skill2()
    {

    }
}
