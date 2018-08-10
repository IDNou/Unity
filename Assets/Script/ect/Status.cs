using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    public bool isHPHealing;
    public bool isMPHealing;
    public float HPRecoveryMount;
    public float MPRecoveryMount;

    private UIProgressBar uiProgressBar;
    private UIProgressBar myProgressBar;
    private UILabel uiGoldText;
    private UILabel myGoldText;

    private bool isPlaying = true;

    private void Start()
    {
        string name = null;
        //여기서 몬스터 및 캐릭터의 데이터베이스를 읽어와서 세팅해준다.
        if (this.GetComponent<PlayerControl>())
        {
            name = "prod";
        }
        else if(this.GetComponent<AkmaControl>())
        {
            name = "akma";
           
        }
        else if(this.GetComponent<MinionContol>())
        {
            name = "gun";
          
        }
        else if(this.GetComponent<FarMinionAnim>())
        {
            name = "one";
           
        }
        else if(this.GetComponent<TowerContol>())
        {
            name = "tower";
        }
        else if(this.tag == "Tree")
        {
            name = "tree";
        }

        Level = (float)LoadManager.Instance.StatusJson[name]["level"];
        HP = (float)LoadManager.Instance.StatusJson[name]["HP"];
        MAXHP = (float)LoadManager.Instance.StatusJson[name]["MAXHP"];
        MP = (float)LoadManager.Instance.StatusJson[name]["MP"];
        MAXMP = (float)LoadManager.Instance.StatusJson[name]["MAXMP"];
        ATK = (float)LoadManager.Instance.StatusJson[name]["ATK"];
        DEF = (float)LoadManager.Instance.StatusJson[name]["DEF"];
        SPD = (float)LoadManager.Instance.StatusJson[name]["SPD"];
        EXP = (float)LoadManager.Instance.StatusJson[name]["EXP"];
        CUREXP = (float)LoadManager.Instance.StatusJson[name]["CUREXP"];
        MAXEXP = (float)LoadManager.Instance.StatusJson[name]["MAXEXP"];

        isHPHealing = false;
        isMPHealing = false;
        HPRecoveryMount = 0;
        MPRecoveryMount = 0;

        if (this.tag != "Tree") // 수정중
        {
            uiProgressBar = Resources.Load<UIProgressBar>("Progress Bar");
            uiGoldText = Resources.Load<UILabel>("GoldText");
            myProgressBar = Instantiate(uiProgressBar, GameObject.Find("ProgressPanel").transform);
            myProgressBar.GetComponent<FollowProgressBar>().target = this.transform.Find("ProgressBarPos").gameObject;
            myProgressBar.GetComponent<FollowProgressBar>().FillHP();
        }
    }

    private void Update()
    {
        if (GameObject.FindGameObjectsWithTag("NaelTower").Length <= 0 || GameObject.FindGameObjectsWithTag("UndeadTower").Length <= 0)
        {
            isPlaying = false;
            if(this.GetComponent<NavMeshAgent>())
                this.GetComponent<NavMeshAgent>().speed = 0;
            if (this.GetComponentInChildren<Animation>())
                this.GetComponentInChildren<Animation>().enabled = false;
            if (this.GetComponentInChildren<Animator>())
                this.GetComponentInChildren<Animator>().enabled = false;
        }

        if (isPlaying)
        {
            if (HP <= 0.0f)
            {
                if (Marker && Marker.name == "Prod" && this.tag != "Tree")
                {
                    GameObject.Find("_GameManager").GetComponent<GameManager>().nGold += 10; // 떨어지는돈 제어해야함

                    myGoldText = Instantiate(uiGoldText, GameObject.Find("HUDGoldPanel").transform);
                    myGoldText.GetComponent<GoldText>().target = this.gameObject;
                    myGoldText.text = "+10Gold";
                }

                if (this.tag == "NaelMinion")
                {
                    if (GameObject.Find("Akma"))
                        GameObject.Find("Akma").GetComponent<Status>().CUREXP += EXP;
                }
                else if (this.tag == "UndeadMinion")
                {
                    if (GameObject.Find("Prod"))
                        GameObject.Find("Prod").GetComponent<Status>().CUREXP += EXP;
                }
                else if (this.tag == "Player")
                {
                    GameObject.Find("Akma").GetComponent<Status>().CUREXP += GameObject.Find("Prod").GetComponent<Status>().Level * 50;
                }
                else if (this.tag == "Akma")
                {
                    GameObject.Find("Prod").GetComponent<Status>().CUREXP += GameObject.Find("Akma").GetComponent<Status>().Level * 50;
                }

                if (this.tag == "NaelMinion" || this.tag == "UndeadMinion" || this.tag == "NaelTower" || this.tag == "UndeadTower") // 수정중
                {
                    this.gameObject.SetActive(false);
                    myProgressBar.gameObject.SetActive(false);
                    Destroy(this.gameObject, 2);
                    Destroy(myProgressBar.gameObject, 2);
                }
                else
                {
                    if (this.tag == "Player")
                    {
                        this.gameObject.SetActive(false);
                        myProgressBar.gameObject.SetActive(false);
                        GameManager.Instance.isPlayerDie = true;
                        //프로그래스바 설정해줘야한다
                    }
                    else if (this.tag == "Enermy")
                    {
                        this.gameObject.SetActive(false);
                        myProgressBar.gameObject.SetActive(false);
                        GameManager.Instance.isEnermyDie = true;
                    }
                }
            }

            if (CUREXP >= MAXEXP && Level <= 18)
            {
                LevelUP();
            }

            if (isHPHealing) SlowHeal();
            if (isMPHealing) SlowMana();
        }
    }

    private void LevelUP()
    {
        SoundManager.Instance.EFXPlaySound("Levelup");
        Level++;
        if (Level <= 18)
        {
            CUREXP -= MAXEXP;
            MAXEXP *= 1.7f;
            MAXHP += 50;
            MAXMP += 50;
            HP += 50;
            MP += 50;
            ATK += 5;
        }
        if (!this.transform.Find("LevelupEffect"))
        {
            GameObject LevelupEffect = (GameObject)Instantiate(Resources.Load<GameObject>("Particle/Levelup"), this.transform);
            LevelupEffect.name = "LevelupEffect";
        }
        else
            this.transform.Find("LevelupEffect").GetComponent<ParticleSystem>().Play();
    }

    private void SlowHeal()
    {

        float healHp = Time.deltaTime * 5.0f; // 한 프레임 당 회복량 (초당 10회복)

        // 현재 남은 회복량이 한프레임 회복량 보다 작은 경우
        if(HP >= MAXHP || HP <= 0)
        {
            isHPHealing = false;
        }

        if (HPRecoveryMount <= healHp)
        {
            HP += HPRecoveryMount;   // 남은 회복량 만큼만 회복
            isHPHealing = false;
        }
        else
            HP += healHp;       // 한프레임 당 회복량 만큼 회복

        HPRecoveryMount -= healHp;
    }

    private void SlowMana()
    {
        float healMp = Time.deltaTime * 5.0f; // 한 프레임 당 회복량 (초당 10회복)

        // 현재 남은 회복량이 한프레임 회복량 보다 작은 경우
        if (MP >= MAXMP || MP <= 0)
        {
            isMPHealing = false;
        }

        if (MPRecoveryMount <= healMp)
        {
            MP += MPRecoveryMount;   // 남은 회복량 만큼만 회복
            isMPHealing = false;
        }
        else
            MP += healMp;       // 한프레임 당 회복량 만큼 회복

        MPRecoveryMount -= healMp;
    }

    private void FastHeal(float HealMount)
    {
        if (HP + HealMount <= MAXHP)
            HP += HealMount;
        else
            HP = MAXHP;

        if (MP + HealMount <= MAXMP)
            MP += HealMount;
        else
            MP = MAXMP;
    }

    private void FullStatus()
    {
        HP = MAXHP;
        MP = MAXMP;
    }

    private void SetActiveMyProgressBar()
    {
        myProgressBar.gameObject.SetActive(true);
    }
}
