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

    public bool isHealing;
    public float RecoveryMount;

    private UIProgressBar uiProgressBar;
    private UIProgressBar myProgressBar;
    private UILabel uiGoldText;
    private UILabel myGoldText;

    private void Start()
    {
        //여기서 몬스터 및 캐릭터의 데이터베이스를 읽어와서 세팅해준다.

        isHealing = false;
        RecoveryMount = 0;

        uiProgressBar = Resources.Load<UIProgressBar>("Progress Bar");
        uiGoldText = Resources.Load<UILabel>("GoldText");
        myProgressBar = Instantiate(uiProgressBar, GameObject.Find("ProgressPanel").transform);
        myProgressBar.GetComponent<FollowProgressBar>().target = this.transform.Find("ProgressBarPos").gameObject;
        myProgressBar.GetComponent<FollowProgressBar>().FillHP();
    }

    private void Update()
    {
        if(HP <= 0.0f)
        {
            if(Marker && Marker.name == "Prod")
            {
                GameObject.Find("_GameManager").GetComponent<GameManager>().nGold += 10;

                myGoldText = Instantiate(uiGoldText, GameObject.Find("HUDGoldPanel").transform);
                myGoldText.GetComponent<GoldText>().target = this.gameObject;
                myGoldText.text = "+10Gold"; 
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

        if (isHealing) SlowHeal();
    }

    private void LevelUP()
    {
        Level++;
        CUREXP -= MAXEXP ;
        MAXEXP *= 1.7f;
        //레벨업 이펙트
    }

    private void SlowHeal()
    {

        float healHp = Time.deltaTime * 5.0f; // 한 프레임 당 회복량 (초당 10회복)

        // 현재 남은 회복량이 한프레임 회복량 보다 작은 경우
        if(HP >= MAXHP || HP <= 0)
        {
            isHealing = false;
        }

        if (RecoveryMount <= healHp)
        {
            HP += RecoveryMount;   // 남은 회복량 만큼만 회복
            isHealing = false;
        }
        else
            HP += healHp;       // 한프레임 당 회복량 만큼 회복

        RecoveryMount -= healHp;
    }
}
