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
        SoundManager.Instance.EFXPlaySound("ImpaleHit");
        GameObject Impail = Instantiate(goImpail);
        Impail.transform.position = this.gameObject.transform.position;
        Impail.transform.rotation = Quaternion.Euler(new Vector3(-90.0f, this.transform.root.transform.eulerAngles.y - 90.0f, 0));
        Impail.GetComponentInChildren<Impail>().Master = this.transform.parent.gameObject;
        this.GetComponentInParent<Status>().MP -= 80;
        this.GetComponentInParent<AkmaControl>().isPossibleImpail = false;
    }

    public void Skill2()
    {
        if (this.GetComponentInParent<AkmaControl>().Enermy)
        {
            SoundManager.Instance.EFXPlaySound("InfernalBirth");
            GameObject Meteo = Instantiate(FireBall);
            Meteo.transform.position = new Vector3(FirePos.position.x, FirePos.position.y + 10, FirePos.position.z);
            Vector3 dir = this.GetComponentInParent<AkmaControl>().Enermy.transform.position - Meteo.transform.position;
            dir.Normalize();
            Meteo.GetComponent<Attack>().startDirection = dir;
            Meteo.GetComponent<Attack>().fSpeed = 10.0f;
            Meteo.GetComponent<Attack>().ATK = 100.0f;
            Meteo.GetComponent<Attack>().Enermy = this.GetComponentInParent<AkmaControl>().Enermy;
            Meteo.GetComponent<Attack>().Master = this.transform.parent.gameObject;
            this.GetComponentInParent<Status>().MP -= 80;
            this.GetComponentInParent<AkmaControl>().isPossibleMeteo = false;
        }
    }
}
