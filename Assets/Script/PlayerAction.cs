using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public Transform FirePos;
    public GameObject FireBall;

    private void Attack()
    {
        if (this.GetComponentInParent<PlayerControl>().Enermy)
        {
            GameObject test = Instantiate(FireBall);
            test.transform.position = FirePos.position;
            Vector3 dir = this.GetComponentInParent<PlayerControl>().Enermy.transform.position - FirePos.position;
            dir.Normalize();
            test.GetComponent<Fireball>().startDirection = dir;
            test.GetComponent<Fireball>().startMagnitude = 100.0f;
            test.GetComponent<Fireball>().ATK = 20.0f;
            test.GetComponent<Fireball>().Enermy = this.GetComponentInParent<PlayerControl>().Enermy;
        }
    }

    private void Meteo()
    {

    }

    private void PowerMeteo()
    {

    }
}
