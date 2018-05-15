using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public Transform FirePos;
    public GameObject FireBall;

    private UIPanel uiPanel;

    private void Awake()
    {
        uiPanel = UIPanel.Find(GameObject.Find("SkillPanel").transform);
    }

    private void Attack()
    {
        if (this.GetComponentInParent<PlayerControl>().Enermy)
        {
            GameObject baseAttack = Instantiate(FireBall);
            baseAttack.transform.position = FirePos.position;
            Vector3 dir = this.GetComponentInParent<PlayerControl>().Enermy.transform.position - FirePos.position;
            dir.Normalize();
            baseAttack.GetComponent<Fireball>().startDirection = dir;
            baseAttack.GetComponent<Fireball>().startMagnitude = 100.0f;
            baseAttack.GetComponent<Fireball>().ATK = 20.0f;
            baseAttack.GetComponent<Fireball>().Enermy = this.GetComponentInParent<PlayerControl>().Enermy;
        }
    }

    private void Meteo()
    {
        if (this.GetComponentInParent<PlayerControl>().Enermy)
        {

        }
    }

    private void RainOfFire()
    {

    }

    private void PowerMeteo()
    {
       //여기서부터 파워메테오 UIButton을 찾아서 fillMode에 연결시켜줘야한다
       //그래야 Cooltime 함수를 SendMessage로 접근할수있는지 없는지 테스트가 가능함
    }
}
