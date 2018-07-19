using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerControl : MonoBehaviour
{
    private NavMeshAgent navMesh = null;
    private Vector3 Dest;
    private Animation anim;
    public GameObject Enermy;
    public GameObject goIndicator;
    public AnimationClip IDLE;
    public AnimationClip ATTACK;
    public AnimationClip MOVE;
    public AnimationClip SKILL1;
    public AnimationClip DEATH;
    public AnimationClip STURN;

    public UIButton btnMeteo;
    public UIButton btnRainOfFire;
    public UIButton btnRequidFire;
    public UIButton btnPowerMeteo;

    private Texture2D attackCursorTexture;
    private Texture2D mouseCursorTexture;
    private Vector2 hotSpot;

    private bool isAttackCursor = false;
    private bool isAttack = false;
    private bool isMove = false;
    private bool isCharge = true;
    private bool isIndicate = false;

    private bool isBaseAttack = false;
    private bool isMeteo = false;
    private bool isRainOfFire = false;
    private bool isPowerMeteo = false;

    private float animDelay = 0;

    private void Awake()
    {
        navMesh = this.GetComponent<NavMeshAgent>();
        anim = this.GetComponentInChildren<Animation>();
        goIndicator.SetActive(false);
        attackCursorTexture = Resources.Load<Texture2D>("Textures/AttackCursor"); // 아마도 게임매니저로 갈뜻?
        mouseCursorTexture = Resources.Load<Texture2D>("Textures/MouseCursor"); // 이것도 게임매니저로 갈뜻
    }

    private void Update()
    {
        //RayCastingEnermy(); 좀더 손좀봐야할뜻

        if(Input.GetKeyDown(KeyCode.G))
        {
            Ray ray = GameObject.Find("UI Root").GetComponentInChildren<Camera>().ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if(Physics.Raycast(ray,out hit))
            {
                print(hit.collider.name);
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Ray ray = GameObject.Find("UI Root").GetComponentInChildren<Camera>().ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (!Physics.Raycast(ray, out hit))
            {
                RayCastingToDiscriminate();
            }
        }
        else if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(isAttackCursor)
            {
                isAttackCursor = false;
                //생각에 수정해야할수가 있음 적클릭했을때 아마 오류가날꺼같음
                RayCastingToDiscriminate();
                ChaingeCursor(false);
            }
            else if(isMeteo)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if(hit.collider.gameObject.tag == "UndeadMinion")
                    {
                        Enermy = hit.collider.gameObject;

                        //if (Enermy.GetComponent<Status>() && Enermy.GetComponent<Status>().nHP < 0)
                        //    Enermy = null;

                        if (Enermy)
                        {
                            if (Vector3.Distance(this.transform.position, Enermy.transform.position) < 5.0f)
                            {
                                this.transform.LookAt(Enermy.transform);
                                navMesh.ResetPath();

                                isMove = false;
                                isMeteo = false;
                                ChaingeCursor(false);
                                anim.CrossFade(SKILL1.name);
                                //navMesh.ResetPath();


                                btnMeteo.GetComponentInChildren<FillMode>().SetCoolTime();
                                StartCoroutine(this.GetComponentInChildren<PlayerAction>().StartSkill(1));
                            }
                            else
                            {
                                isMove = true;
                                Dest = Enermy.transform.position;
                                MoveOrder(Dest);
                                anim.CrossFade(MOVE.name);
                            }
                        }
                    }
                    else
                    {
                        ChaingeCursor(false);
                    }
                }
                
            }
            else if(isRainOfFire)
            {
                isIndicate = false;
                isRainOfFire = false;
                ChaingeCursor(false);
                disableIndicator();
                anim.CrossFade(SKILL1.name);
                navMesh.ResetPath();

                StartCoroutine(this.GetComponentInChildren<PlayerAction>().StartSkill(2));
            }
            else if(isPowerMeteo)
            {
                isIndicate = false;
                if (Vector3.Distance(this.transform.position, goIndicator.transform.position) < 5.0f)
                {
                    isPowerMeteo = false;
                    ChaingeCursor(false);
                    disableIndicator();
                    anim.CrossFade(SKILL1.name);
                    navMesh.ResetPath();

                    btnPowerMeteo.GetComponentInChildren<FillMode>().SetCoolTime();
                    StartCoroutine(this.GetComponentInChildren<PlayerAction>().StartSkill(3));
                }
                else
                {
                    isMove = true;
                    Dest = goIndicator.transform.position;
                    MoveOrder(Dest);
                    anim.CrossFade(MOVE.name);

                    ChaingeCursor(false);
                    disableIndicator();
                }
            }
            else
            {
                //적 스테이터스창 보여주게해야함
            }
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            isAttack = false;

            isAttackCursor = true;
            ChaingeCursor(true);
        }
        else if(btnMeteo.GetComponentInChildren<FillMode>().AbleSkil() && Input.GetKeyDown(KeyCode.T)) //메테오
        {
            isAttack = false;

            isMeteo = true;
            ChaingeCursor(true);
        }
        else if(btnRequidFire.GetComponentInChildren<FillMode>().AbleSkil() && Input.GetKeyDown(KeyCode.F)) // 레인파이어
        {
            isAttack = false;
            isIndicate = true;
            isRainOfFire = true;
        }
        else if(btnPowerMeteo.GetComponentInChildren<FillMode>().AbleSkil() && Input.GetKeyDown(KeyCode.W)) // 팜
        {
            isAttack = false;
            isIndicate = true;
            isPowerMeteo = true;
        }

        if (Enermy)
        {
            //if (Vector3.Distance(this.transform.position, Enermy.transform.position) < 3.0f)
            //{
            //    isAttack = true;
            //    this.transform.LookAt(Enermy.transform);
            //    navMesh.ResetPath();
            //}
            //else
            //{
            //    isMove = true;
            //    Dest = Enermy.transform.position;
            //    MoveOrder(Dest);
            //    anim.CrossFade(MOVE.name);
            //}
            if (Enermy.GetComponent<Status>() && Enermy.GetComponent<Status>().HP < 0)
            {
                Enermy = null;
                isMove = false;
                isAttack = false;
            }

            if (isMove)
            {
                Dest = Enermy.transform.position;
                MoveOrder(Dest);

              
                if (isBaseAttack)
                {
                    if (Vector3.Distance(this.transform.position, Enermy.transform.position) < 3.0f)
                    {
                        isMove = false;
                        isBaseAttack = false;
                        isAttack = true;

                        this.transform.LookAt(Enermy.transform);
                        navMesh.ResetPath();
                    }
                }
                else if(isMeteo)
                {
                    if (Vector3.Distance(this.transform.position, Enermy.transform.position) < 5.0f)
                    {
                        isMove = false;
                        isMeteo = false;
                        ChaingeCursor(false);
                        anim.CrossFade(SKILL1.name);
                        navMesh.ResetPath();

                        btnMeteo.GetComponentInChildren<FillMode>().SetCoolTime();
                        StartCoroutine(this.GetComponentInChildren<PlayerAction>().StartSkill(1));

                        this.transform.LookAt(Enermy.transform);
                        navMesh.ResetPath();
                    }
                }

            }
           

            if (isAttack)
            {
                if (isCharge)
                {
                    isCharge = false;
                    animDelay = ATTACK.length * 2.0f;
                    if (Vector3.Distance(this.transform.position, Enermy.transform.position) < 3.0f)
                    {
                        this.transform.LookAt(Enermy.transform);
                        anim.CrossFade(ATTACK.name); // 발사
                    }
                    else
                    {
                        isMove = true;
                        isBaseAttack = true;
                        Dest = Enermy.transform.position;
                        MoveOrder(Dest);
                        anim.CrossFade(MOVE.name);
                    }
                }
                else
                {
                    this.transform.LookAt(Enermy.transform);
                    //요기는 쪼꼼나중에 해보자!
                    //if (Vector3.Distance(this.transform.position, Enermy.transform.position) < 3.0f)
                    //{
                    //    anim.CrossFade(IDLE.name);
                    //}
                }
            }

            if (Enermy && !Enermy.activeSelf)
            {
                anim.CrossFade(IDLE.name);
                Enermy = null;
            }
        }
        else
        {
            if (isPowerMeteo && !isIndicate)
            {
                if (Vector3.Distance(this.transform.position, goIndicator.transform.position) < 5.0f)
                {
                    isMove = false;
                    isPowerMeteo = false;
                    ChaingeCursor(false);
                    disableIndicator();
                    anim.CrossFade(SKILL1.name);
                    navMesh.ResetPath();

                    btnPowerMeteo.GetComponentInChildren<FillMode>().SetCoolTime();
                    StartCoroutine(this.GetComponentInChildren<PlayerAction>().StartSkill(3));

                    this.transform.LookAt(goIndicator.transform);
                    navMesh.ResetPath();
                }
            }
        }

        if (animDelay < 0)
            isCharge = true;
        else
            animDelay -= Time.deltaTime;

        // 그위치로 순간이동
        if (isMove && Vector3.Distance(this.transform.position, Dest) < 0.1f)
        {
            isMove = false;
            this.transform.position = Dest;
            navMesh.ResetPath();
            anim.CrossFade(IDLE.name);
        }
    }

    private void FixedUpdate()
    {
        if (isIndicate)
        {
            enabledIndicator();
        }
    }

    private void RayCastingEnermy()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag == "UndeadMinion" || hit.collider.gameObject.tag == "Tree")
            {
                ChaingeCursor(true);
            }
            else
            {
                ChaingeCursor(false);
            }
        }
    }

    private void RayCastingToDiscriminate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag == "UndeadMinion" || hit.collider.gameObject.tag == "UndeadTower"|| hit.collider.gameObject.tag == "Tree")
            {
                Enermy = hit.collider.gameObject;

                if (Enermy.GetComponent<Status>() && Enermy.GetComponent<Status>().HP < 0)
                    Enermy = null;

                if (Enermy)
                {
                    if (Vector3.Distance(this.transform.position, Enermy.transform.position) < 3.0f)
                    {
                        isAttack = true;
                        isMove = false;
                        this.transform.LookAt(Enermy.transform);
                        navMesh.ResetPath();
                    }
                    else
                    {
                        isMove = true;
                        isBaseAttack = true;
                        Dest = Enermy.transform.position;
                        MoveOrder(Dest);
                        anim.CrossFade(MOVE.name);
                    }
                }
            }
            else
            {
                isIndicate = false;
                disableIndicator();
                isAttackCursor = false;
                isMeteo = false;
                isPowerMeteo = false;
                ChaingeCursor(false);

                Dest = hit.point;
                isMove = true;
                isAttack = false;
                Enermy = null;
                MoveOrder(Dest);
                anim.CrossFade(MOVE.name);
            }
        }
    }

    private void ChaingeCursor(bool isAttack)
    {
        if (isAttack)
        {
            hotSpot.x = 0;
            hotSpot.y = 0;
            Cursor.SetCursor(attackCursorTexture, hotSpot, CursorMode.Auto);
        }
        else
        {
            hotSpot.x = 0;
            hotSpot.y = 0;
            Cursor.SetCursor(mouseCursorTexture, hotSpot, CursorMode.Auto);
        }
    }

    private void enabledIndicator()
    {
        goIndicator.SetActive(true);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray,out hit,100,(1<<8)))
        {
            goIndicator.transform.position = hit.point;
        }
    }

    private void disableIndicator()
    {
        goIndicator.SetActive(false);
    }

    private void MoveOrder(Vector3 dest)
    {
        navMesh.SetDestination(dest);   // 목적지 설정
    }

    private void OnDrawGizmos()
    {
       if(navMesh)
       {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(this.transform.position, this.transform.position + navMesh.velocity);

            for(int i=0; i<navMesh.path.corners.Length; ++i)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(navMesh.path.corners[i], 1.0f);
            }
       }

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, 3.0f);
    }
}
