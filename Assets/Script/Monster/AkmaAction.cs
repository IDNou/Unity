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
            GameObject baseAttack = Instantiate(FireBall);
            baseAttack.transform.position = FirePos.position;
            Vector3 dir = this.GetComponentInParent<AkmaControl>().Enermy.transform.position - FirePos.position;
            dir.Normalize();
            baseAttack.GetComponent<Attack>().startDirection = dir;
            baseAttack.GetComponent<Attack>().fSpeed = 5.0f;
            baseAttack.GetComponent<Attack>().ATK = this.GetComponentInParent<Status>().ATK; // 공격력 표기를 어디다가 해야할지 결정해야할뜻
            baseAttack.GetComponent<Attack>().Enermy = this.GetComponentInParent<AkmaControl>().Enermy;
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
