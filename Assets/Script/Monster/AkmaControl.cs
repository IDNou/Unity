using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AkmaControl : MonoBehaviour
{
    private NavMeshAgent navMesh = null;
    private Animator anim;
    public GameObject Enermy;

    private float fAttackMotionSpeed = 0.0f;

    private bool isMove = false;
    private bool isAttack = false; // 공격중인가?
    private bool isGoAttack = false; // 미니언 있으니 공격해도된다
    public bool isAttackCharge;
    private bool isAbsoluteForward; //앞으로 절대적으로 전진해서 때리게끔하기위해서
    private bool isDest;

    public float impailCoolTime;
    public bool isPossibleImpail;
    public float meteoCoolTime;
    public bool isPossibleMeteo;
    public float powerMeteoCoolTime;
    private bool isPossiblePowerMeteo;

    private bool isImmediatelyEscape;
    private bool isSafe;

    private Vector3 Dest;

    private void Awake()
    {
        navMesh = this.GetComponent<NavMeshAgent>();
        anim = this.GetComponentInChildren<Animator>();
        isAttackCharge = true;
        isAbsoluteForward = false;
        isDest = false;
        isPossibleImpail = true;
        isPossibleMeteo = true;
        isImmediatelyEscape = false;
        isSafe = false;
    }

    private void Update()
    {
        //차징
        if (!isAttackCharge)
        {
            if (fAttackMotionSpeed > 0)
            {
                fAttackMotionSpeed -= Time.deltaTime;
            }
            else
            {
                fAttackMotionSpeed = 3.0f;
                isAttackCharge = true;
            }
        }

        if(!isPossibleImpail)
        {
            if(impailCoolTime <= 0)
            {
                isPossibleImpail = true;
                impailCoolTime = 9.0f;
            }
            else
                impailCoolTime -= Time.deltaTime;

        }
        if(!isPossibleMeteo)
        {
            if (meteoCoolTime <= 0)
            {
                isPossibleMeteo = true;
                meteoCoolTime = 9.0f;
            }
            else
                meteoCoolTime -= Time.deltaTime;
        }

        if(!isImmediatelyEscape && this.GetComponent<Status>().HP / this.GetComponent<Status>().MAXHP <= 0.2f)
        {
            isImmediatelyEscape = true;
            isSafe = false;
            //print("긴급탈출");
            if (GameObject.FindGameObjectWithTag("UndeadTower"))
            {
                Vector3 towerPos = GameObject.FindGameObjectWithTag("UndeadTower").transform.position;
                Dest = new Vector3(Random.Range(towerPos.x - 2, towerPos.x), towerPos.y, Random.Range(towerPos.z, towerPos.z - 1));
            }
            if (!navMesh.hasPath)
                MoveToDestination(Dest);
        }


        if (!GameManager.Instance.isEnermyDie)
        {
            if (!isImmediatelyEscape)
            {
                if (this.GetComponent<Status>().HP / this.GetComponent<Status>().MAXHP <= 0.3f)
                {
                    this.GetComponent<Status>().isHPHealing = true;
                    this.GetComponent<Status>().secondPerRecovery = 20.0f;
                    this.GetComponent<Status>().HPRecoveryMount = 300.0f;
                }
                else if (this.GetComponent<Status>().MP / this.GetComponent<Status>().MAXMP <= 0.3f)
                {
                    this.GetComponent<Status>().isMPHealing = true;
                    this.GetComponent<Status>().secondPerRecovery = 20.0f;
                    this.GetComponent<Status>().MPRecoveryMount = 300.0f;
                }

                if (isMove) // 움직이고 있는가?
                {
                    if (isGoAttack) //미니언하고 같이 움직이는건가?
                    {
                        if (SearchForwardMinion("UndeadMinion"))
                        {
                            //print("전진");
                            if (GameObject.FindGameObjectWithTag("NaelTower"))
                            {
                                Vector3 towerPos = GameObject.FindGameObjectWithTag("NaelTower").transform.position;
                                Dest = new Vector3(Random.Range(towerPos.x, towerPos.x + 2), towerPos.y, Random.Range(towerPos.z, towerPos.z - 1));
                                MoveToDestination(Dest);
                            }
                        }
                        else
                        {
                            //print("뒤미니언 찾으러 간다");
                            if (GameObject.FindGameObjectWithTag("UndeadTower"))
                            {
                                Vector3 towerPos = GameObject.FindGameObjectWithTag("UndeadTower").transform.position;
                                Dest = new Vector3(Random.Range(towerPos.x - 2, towerPos.x), towerPos.y, Random.Range(towerPos.z, towerPos.z - 1));
                                MoveToDestination(Dest);
                            }

                        }

                        if (!isAbsoluteForward && (SearchGameObject("NaelMinion") || SearchGameObject("NaelTower") || SearchGameObject("Player"))) //공격대상을 넣어준다
                        {
                            //print("스탑후 공격");
                            isMove = false;
                            isAttack = true;
                            navMesh.ResetPath();
                        }
                        isAbsoluteForward = false;
                    }
                    else
                    {
                        if (!isDest && SearchForwardMinion("UndeadMinion")) // 움직이고있지만 미니언이랑 있는지 몰라서 검색해봄 실질적으로 공격하러가야하는곳
                        {
                            isGoAttack = true;
                            if (!navMesh.hasPath)
                            {
                                if (GameObject.FindGameObjectWithTag("NaelTower"))
                                {
                                    Vector3 towerPos = GameObject.FindGameObjectWithTag("NaelTower").transform.position;
                                    Dest = new Vector3(Random.Range(towerPos.x, towerPos.x + 2), towerPos.y, Random.Range(towerPos.z, towerPos.z - 1));
                                    MoveToDestination(Dest);
                                }
                            }
                        }
                    }

                    if (Vector3.Distance(this.transform.position, Dest) <= 0.2f) // 목적지에 도착했는가?
                    {
                        isMove = false;
                        isAttack = false;
                        this.transform.position = Dest;
                        isDest = false;
                        navMesh.ResetPath();
                        anim.SetFloat("isMove", 0);
                    }
                }
                else
                {
                    if (isAttack)
                    {
                        if (SearchForwardMinion("UndeadMinion")) //내주변에 미니언이 있는가?
                        {
                            //스킬있는대로 다퍼붓자
                            //if Enermy가 죽었다면 다시 앞으로 가며 탐색

                            SearchEnermy();

                        }
                        else // 내가 너무 앞에 나와있거나 미니언이없다.
                        {
                            //print("졸렬");
                            //졸렬하게 도망가자
                            isGoAttack = false;
                            isMove = true;
                            if (GameObject.FindGameObjectWithTag("UndeadTower"))
                            {
                                Vector3 towerPos = GameObject.FindGameObjectWithTag("UndeadTower").transform.position;
                                Dest = new Vector3(Random.Range(towerPos.x - 2, towerPos.x), towerPos.y, Random.Range(towerPos.z, towerPos.z - 1));
                            }
                            if (!navMesh.hasPath)
                                MoveToDestination(Dest);
                        }
                    }
                    else
                    {
                        if (SearchGameObject("UndeadTower")) // 타워 근처이다
                        {
                            if (SearchForwardMinion("UndeadMinion")) // 내 주변에 미니언이 있는가?
                            {
                                //공격하러 가즈아
                                isMove = true;
                                isGoAttack = true;
                                if (GameObject.FindGameObjectWithTag("NaelTower"))
                                {
                                    Vector3 towerPos = GameObject.FindGameObjectWithTag("NaelTower").transform.position;
                                    Dest = new Vector3(Random.Range(towerPos.x, towerPos.x + 2), towerPos.y, Random.Range(towerPos.z, towerPos.z - 1));
                                }
                                if (!navMesh.hasPath)
                                    MoveToDestination(Dest);
                            }
                            else //없다
                            {
                                SearchEnermy();
                            }
                        }
                        else // 움직임도없고 공격중이지도 않으며 타워주변이 아니므로 타워로 돌아간다.
                        {
                            //print("돌아간다");
                            isMove = true;
                            if (GameObject.FindGameObjectWithTag("UndeadTower"))
                            {
                                if (!isDest)
                                {
                                    Vector3 towerPos = GameObject.FindGameObjectWithTag("UndeadTower").transform.position;
                                    Dest = new Vector3(Random.Range(towerPos.x - 2, towerPos.x), towerPos.y, Random.Range(towerPos.z, towerPos.z - 1));
                                    isDest = true;
                                }
                            }
                            if (!navMesh.hasPath)
                                MoveToDestination(Dest);
                        }

                    }
                }
            }
            else
            {
                if (!isSafe && Vector3.Distance(this.transform.position, Dest) <= 0.2f) // 목적지에 도착했는가?
                {
                    this.transform.position = Dest;
                    navMesh.ResetPath();
                    anim.SetFloat("isMove", 0);
                    anim.SetBool("isSkill1", false);
                    anim.SetBool("isSkill2", false);
                    anim.SetBool("isAttack", false);

                    this.GetComponent<Status>().isHPHealing = true;
                    this.GetComponent<Status>().secondPerRecovery = 40.0f;
                    this.GetComponent<Status>().HPRecoveryMount = this.GetComponent<Status>().MAXHP;

                    this.GetComponent<Status>().isMPHealing = true;
                    this.GetComponent<Status>().secondPerRecovery = 30.0f;
                    this.GetComponent<Status>().MPRecoveryMount = this.GetComponent<Status>().MAXMP;

                    isSafe = true;
                }

                if (isSafe)
                    SearchEnermy();

                if (this.GetComponent<Status>().HP /this.GetComponent<Status>().MAXHP  >= 0.9f)
                {
                    ResetBool();
                    isImmediatelyEscape = false;
                }
            }
        }
    }

    private bool SearchGameObject(string tag)
    {
        bool SerachGO = false;

        Collider[] colliders;
        colliders = Physics.OverlapSphere(this.transform.position, 3.0f);
        foreach (Collider col in colliders)
        {
            if (col.gameObject.tag == tag)
                SerachGO = true;
        }

        return SerachGO;
    }

    private bool SearchForwardMinion(string tag)
    {
        bool forwardMinion = false;

        Collider[] colliders;
        colliders = Physics.OverlapSphere(this.transform.position, 3.0f);
        foreach (Collider col in colliders)
        {
            if (col.gameObject.tag == tag && col.gameObject.transform.position.x > this.transform.position.x)
                forwardMinion = true;
        }

        return forwardMinion;
    }

    private void MoveToDestination(Vector3 Dest)
    {
        navMesh.SetDestination(Dest);
        anim.SetBool("isAttack", false);
        anim.SetFloat("isMove", navMesh.speed);
    }

    private void SearchEnermy()
    {
        float dist = 9999;

        Collider[] colliders;
        colliders = Physics.OverlapSphere(this.transform.position, 3.0f);
        foreach (Collider col in colliders)
        {
            if (col.gameObject.tag == "NaelMinion" || col.gameObject.tag == "Player" || col.gameObject.tag == "NaelTower")
            {
                if (Vector3.Distance(this.transform.position, col.gameObject.transform.position) < dist)
                {
                    dist = Vector3.Distance(this.transform.position, col.gameObject.transform.position);
                    Enermy = col.gameObject;
                }
            }
        }

        if (Enermy)
        {
            if (Enermy.activeSelf == false || Enermy.GetComponent<Status>().HP <= 0)
            {
                Enermy = null;
                isAttack = false;
                isMove = true;
                isGoAttack = true;
            }
            else
            {
                if (Vector3.Distance(this.transform.position, Enermy.transform.position) <= 3.0f)
                {
                    //print("공격");
                    anim.SetFloat("isMove", 0);
                    this.transform.LookAt(Enermy.transform);

                    if (isPossibleImpail && Enermy.tag != "NaelTower" && this.GetComponent<Status>().MP >= 80)
                    {
                        anim.SetBool("isSkill1", true);
                    }
                    else
                    {
                        anim.SetBool("isSkill1", false);
                    }

                    if (isPossibleMeteo && Enermy.tag != "NaelTower" && this.GetComponent<Status>().MP >= 80)
                    {
                        anim.SetBool("isSkill2", true);
                    }
                    else
                    {
                        anim.SetBool("isSkill2", false);
                    }

                    if (isAttackCharge)
                    {
                        anim.SetBool("isAttack", true);
                    }
                    else
                    {
                        anim.SetBool("isAttack", false);
                    }
                }
                else
                {
                    //print("거리가 안되서 못공격");
                    Enermy = null;
                    isAttack = false;
                    isMove = true;
                    isGoAttack = true;
                    isAbsoluteForward = true;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (navMesh)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(this.transform.position, this.transform.position + navMesh.velocity);

            for (int i = 0; i < navMesh.path.corners.Length; ++i)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(navMesh.path.corners[i], 1.0f);
            }
        }

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, 3.0f);
    }

    private void ResetBool()
    {
        isMove = false;
        isAttack = false; // 공격중인가?
        isGoAttack = false; // 미니언 있으니 공격해도된다
        isAttackCharge = true;
        navMesh.isStopped = true;
        isImmediatelyEscape = false;
    }
}
