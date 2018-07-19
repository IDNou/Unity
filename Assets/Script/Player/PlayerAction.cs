using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public Transform FirePos;
    public GameObject FireBall;
    public GameObject goMeteo;

    private void Awake()
    {
        
    }

    private void Attack()
    {
        if (this.GetComponentInParent<PlayerControl>().Enermy)
        {
            Status myStatus = GetComponentInParent<Status>();
            Status enermyStatus = this.GetComponentInParent<PlayerControl>().Enermy.GetComponent<Status>();
            GameObject baseAttack = Instantiate(FireBall);
            baseAttack.transform.position = FirePos.position;
            Vector3 dir = this.GetComponentInParent<PlayerControl>().Enermy.transform.position - FirePos.position;
            dir.Normalize();
            baseAttack.GetComponent<Attack>().startDirection = dir;
            baseAttack.GetComponent<Attack>().fSpeed = 5.0f;
            baseAttack.GetComponent<Attack>().ATK = (int)(myStatus.ATK - (myStatus.ATK * enermyStatus.DEF / (enermyStatus.DEF + 100)));
            baseAttack.GetComponent<Attack>().Enermy = this.GetComponentInParent<PlayerControl>().Enermy;
            baseAttack.GetComponent<Attack>().Master = this.transform.parent.gameObject;
        }
    }

    private void Meteo()
    {
        if (this.GetComponentInParent<PlayerControl>().Enermy)
        {
            GameObject Meteo = Instantiate(FireBall);
            Meteo.transform.position = new Vector3(FirePos.position.x, FirePos.position.y + 10, FirePos.position.z);
            Vector3 dir = this.GetComponentInParent<PlayerControl>().Enermy.transform.position - Meteo.transform.position;
            dir.Normalize();
            Meteo.GetComponent<Attack>().startDirection = dir;
            Meteo.GetComponent<Attack>().fSpeed = 10.0f;
            Meteo.GetComponent<Attack>().ATK = 100.0f;
            Meteo.GetComponent<Attack>().Enermy = this.GetComponentInParent<PlayerControl>().Enermy;
            Meteo.GetComponent<Attack>().Master = this.transform.parent.gameObject;
        }
    }

    //private void RainOfFire()
    //{
    //    btnRainOfFire.GetComponentInChildren<FillMode>().CoolTime();
    //    print("Rain");
    //}

    private void PowerMeteo()
    {
        //여기서부터 파워메테오 UIButton을 찾아서 fillMode스크립트에 연결시켜줘야한다
        //그래야 Cooltime 함수를 SendMessage로 접근할수있는지 없는지 테스트가 가능함
        
        GameObject PowerMeteo = Instantiate(goMeteo);
        PowerMeteo.transform.position = new Vector3(FirePos.position.x, FirePos.position.y + 10, FirePos.position.z);
        Vector3 dir = this.GetComponentInParent<PlayerControl>().goIndicator.transform.position - PowerMeteo.transform.position;
        PowerMeteo.GetComponent<Fireball>().startDirection = dir;
        PowerMeteo.GetComponent<Fireball>().ATK = 300.0f;
        PowerMeteo.GetComponent<Fireball>().Destination = this.GetComponentInParent<PlayerControl>().goIndicator.transform.position;
        PowerMeteo.GetComponent<Fireball>().Master = this.transform.parent.gameObject;

    }

    public IEnumerator StartSkill(int value)
    {
        yield return new WaitForSeconds(1.0f);

        switch (value)
        {
            case 1:
                Meteo();
                break;
            case 2:
                //RainOfFire();
                break;
            case 3:
                PowerMeteo();
                break;
            default:
                break;
        }
    }
}
