﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerContol : MonoBehaviour
{
    private float MaxDist;
    private float ChageTime;
    private bool Charge;

    public GameObject Enermy;
    public GameObject FireEffect;
    public Transform FirePos;

    private void Awake()
    {
        Charge = true;
    }

    private void Update()
    {
        MaxDist = 9999;
        Collider[] colliders;
        colliders = Physics.OverlapSphere(this.transform.position, this.GetComponent<SphereCollider>().radius);
        foreach (Collider col in colliders)
        {
            if(this.gameObject.tag == "UndeadTower" && (col.gameObject.tag == "NaelMinion" || col.gameObject.tag == "Player"))
            {
                if (Vector3.Distance(this.transform.position, col.transform.position) < MaxDist)
                {
                    Enermy = col.gameObject;
                    MaxDist = Vector3.Distance(this.transform.position, col.transform.position);
                }
            }
            else if (this.gameObject.tag == "NaelTower" && (col.gameObject.tag == "UndeadMinion" || col.gameObject.tag == "Enermy"))
            {
                if (Vector3.Distance(this.transform.position, col.transform.position) < MaxDist)
                {
                    Enermy = col.gameObject;
                    MaxDist = Vector3.Distance(this.transform.position, col.transform.position);
                }
            }
        }

        if(Enermy)
        {
            if (Charge)
            {
                Charge = false;
                ChageTime = 2.5f;
                GameObject fireball = Instantiate(FireEffect);
                fireball.transform.position = FirePos.transform.position;
                Vector3 dir = Enermy.transform.position - FirePos.transform.position;
                dir.Normalize();
                fireball.GetComponent<Attack>().startDirection = dir;
                fireball.GetComponent<Attack>().Enermy = Enermy;
                fireball.GetComponent<Attack>().ATK = this.GetComponent<Status>().ATK;
                fireball.GetComponent<Attack>().fSpeed = 5.0f;
                fireball.GetComponent<Attack>().Master = this.gameObject;
                SoundManager.Instance.EFXPlaySound("SearingArrowTarget" + Random.Range(1, 4));
            }

            if (!Enermy.activeSelf || Enermy.GetComponent<Status>().HP <= 0|| Vector3.Distance(Enermy.transform.position, this.transform.position) > this.GetComponent<SphereCollider>().radius)
                Enermy = null;
        }

        if(!Charge)
        {
            ChageTime -= Time.deltaTime;

            if (ChageTime < 0)
                Charge = true;
        }
    }

}
