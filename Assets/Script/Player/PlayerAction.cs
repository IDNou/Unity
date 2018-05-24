using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public Transform FirePos;
    public GameObject FireBall;
    public GameObject goMeteo;

    public UIButton btnMeteo;
    public UIButton btnRainOfFire;
    public UIButton btnRequidFire;
    public UIButton btnPowerMeteo;

    private void Awake()
    {
        
    }

    private void Attack()
    {
        if (this.GetComponentInParent<PlayerControl>().Enermy)
        {
            GameObject baseAttack = Instantiate(FireBall);
            baseAttack.transform.position = FirePos.position;
            Vector3 dir = this.GetComponentInParent<PlayerControl>().Enermy.transform.position - FirePos.position;
            dir.Normalize();
            baseAttack.GetComponent<Attack>().startDirection = dir;
            baseAttack.GetComponent<Attack>().startMagnitude = 100.0f;
            baseAttack.GetComponent<Attack>().ATK = 20.0f;
            baseAttack.GetComponent<Attack>().Enermy = this.GetComponentInParent<PlayerControl>().Enermy;
        }
    }

    private void Meteo()
    {
        btnMeteo.GetComponentInChildren<FillMode>().CoolTime();
        print("Meteo");
    }

    private void RainOfFire()
    {
        btnRainOfFire.GetComponentInChildren<FillMode>().CoolTime();
        print("Rain");
    }

    private void PowerMeteo()
    {
        //여기서부터 파워메테오 UIButton을 찾아서 fillMode스크립트에 연결시켜줘야한다
        //그래야 Cooltime 함수를 SendMessage로 접근할수있는지 없는지 테스트가 가능함
        btnPowerMeteo.GetComponentInChildren<FillMode>().CoolTime();

        GameObject PowerMeteo = Instantiate(goMeteo);
        PowerMeteo.transform.position = new Vector3(FirePos.position.x, FirePos.position.y + 10, FirePos.position.z);
        Vector3 dir = this.GetComponentInParent<PlayerControl>().goIndicator.transform.position - PowerMeteo.transform.position;
        dir.Normalize();
        PowerMeteo.GetComponent<Fireball>().startDirection = dir;
        PowerMeteo.GetComponent<Fireball>().Destination = this.GetComponentInParent<PlayerControl>().goIndicator.transform.position;
        PowerMeteo.GetComponent<Fireball>().startMagnitude = 500.0f;

        print("PowerMeteo");
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
                RainOfFire();
                break;
            case 3:
                PowerMeteo();
                break;
            default:
                break;
        }
    }
}
