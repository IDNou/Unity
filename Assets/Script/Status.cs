using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour {

    public float Level;
    public float HP;
    public float MAXHP;
    public float MP;
    public float MAXMP;
    public float ATK;
    public float DEF;
    public float SPD;
    public float EXP;
    public float CUREXP;
    public float MAXEXP;
    public GameObject Marker = null;

    private UIProgressBar uiProgressBar;
    private UIProgressBar myProgressBar;

    private void Start()
    {
        uiProgressBar = Resources.Load<UIProgressBar>("Progress Bar");
        myProgressBar = Instantiate(uiProgressBar, GameObject.Find("ProgressPanel").transform);
        myProgressBar.GetComponent<FollowProgressBar>().target = this.gameObject.GetComponentInChildren<Transform>().Find("ProgressBarPos").gameObject;
        myProgressBar.GetComponent<FollowProgressBar>().FillHP();
    }

    private void Update()
    {
        if(HP <= 0.0f)
        {
            if(Marker && Marker.name == "Prod")
            {
                GameObject.Find("_GameManager").GetComponent<GameManager>().nGold += 10;
            }

            if(this.tag=="NaelMinion")
            {
                GameObject.Find("Akma").GetComponent<Status>().CUREXP += EXP;
            }
            else if(this.tag == "UndeadMinion")
            {
                GameObject.Find("Prod").GetComponent<Status>().CUREXP += EXP;
            }

            this.gameObject.SetActive(false);
            myProgressBar.gameObject.SetActive(false);
            Destroy(this.gameObject, 2);
            Destroy(myProgressBar.gameObject, 2);
        }

        if(CUREXP >= MAXEXP)
        {
            LevelUP();
        }
    }

    private void LevelUP()
    {
        Level++;
        CUREXP -= MAXEXP ;
        MAXEXP *= 1.7f;
        //레벨업 이펙트
    }

    private void SlowHeal(float RecoveryMount)
    {

        float healHp = Time.deltaTime * 10.0f; // 한 프레임 당 회복량 (초당 10회복)

        // 현재 남은 회복량이 한프레임 회복량 보다 작은 경우
        if (RecoveryMount < healHp)
        {
            HP += healAmount;   // 남은 회복량 만큼만 회복
            isHealing = false;
        }
        else
            currHp += healHp;       // 한프레임 당 회복량 만큼 회복


        RecoveryMount -= healHp;
    }
}
