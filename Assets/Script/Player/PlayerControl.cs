using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerControl : MonoBehaviour
{
    private NavMeshAgent navMesh = null;
    private Vector3 Dest;
    private Vector3 DumyDest;
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
    private bool isAttackDDang = false;
    private bool isMove = false;
    private bool isCharge = true;
    private bool isIndicate = false;

    private bool isBaseAttack = false;
    private bool isMeteo = false;
    private bool isRainOfFire = false;
    private bool isPowerMeteo = false;

    private float animDelay = 0;
    private float MaxDist = 9999;

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

        if (Input.GetKeyDown(KeyCode.Mouse1))//우클릭
        {
            Ray ray = GameObject.Find("UI Root").GetComponentInChildren<Camera>().ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (!Physics.Raycast(ray, out hit))
            {
                RayCastingToDiscriminate();
            }
        }
        else if(Input.GetKeyDown(KeyCode.Mouse0)) // 좌클릭
        {
            if(isAttackCursor)
            {
                //생각에 수정해야할수가 있음 적클릭했을때 아마 오류가날꺼같음
                RayCastingToDiscriminate();
                isAttackCursor = false;
                ChaingeCursor(false);
            }
            else if(isMeteo)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if(hit.collider.gameObject.tag == "UndeadMinion" || hit.collider.gameObject.tag == "Enermy")
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
                if (Vector3.Distance(this.transform.position, goIndicator.transform.position) < 5.0f)
                {
                    isIndicate = false;
                    isRainOfFire = false;
                    ChaingeCursor(false);
                    disableIndicator();
                    anim.CrossFade(SKILL1.name);
                    navMesh.ResetPath();

                    this.transform.LookAt(goIndicator.transform.position);
                    btnRainOfFire.GetComponentInChildren<FillMode>().SetCoolTime();
                    StartCoroutine(this.GetComponentInChildren<PlayerAction>().StartSkill(2));
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

                    this.transform.LookAt(goIndicator.transform.position);
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
        else if(btnMeteo.GetComponentInChildren<FillMode>().AbleSkil() /* && this.GetComponent<Status>().MP >= 80 */ && Input.GetKeyDown(KeyCode.T)) //메테오
        {
            if (this.GetComponent<Status>().MP >= 80)
            {
                isAttack = false;
                isMeteo = true;
                ChaingeCursor(true);
            }
            else
            {
                if (!SoundManager.Instance.EFXPlayingSound("NoEnergy"))
                    SoundManager.Instance.EFXPlaySound("NoEnergy");
            }
        }
        else if(btnRainOfFire.GetComponentInChildren<FillMode>().AbleSkil() /* && this.GetComponent<Status>().MP >= 80 */ && Input.GetKeyDown(KeyCode.F)) // 임페일
        {
            if (this.GetComponent<Status>().MP >= 80)
            {
                isAttack = false;
                isIndicate = true;
                isRainOfFire = true;
            }
            else
            {
                if (!SoundManager.Instance.EFXPlayingSound("NoEnergy"))
                    SoundManager.Instance.EFXPlaySound("NoEnergy");
            }
        }
        else if(btnPowerMeteo.GetComponentInChildren<FillMode>().AbleSkil() /* && this.GetComponent<Status>().MP >= 150 */ &&Input.GetKeyDown(KeyCode.W)) // 팜
        {
            if (this.GetComponent<Status>().MP >= 150)
            {
                isAttack = false;
                isIndicate = true;
                isPowerMeteo = true;
            }
            else
            {
                if (!SoundManager.Instance.EFXPlayingSound("NoEnergy"))
                    SoundManager.Instance.EFXPlaySound("NoEnergy");
            }
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
            if (isMove)
            {
                //if (Enermy)
                //{
                //    Dest = Enermy.transform.position;
                //    MoveOrder(Dest);
                //}

              
                if (isBaseAttack)
                {
                    if (Vector3.Distance(this.transform.position, Enermy.transform.position) <= 3.0f)
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
                    if (Vector3.Distance(this.transform.position, Enermy.transform.position) <= 3.0f)
                    {
                        isCharge = false;
                        animDelay = ATTACK.length * 2.0f;
                        this.transform.LookAt(Enermy.transform);
                        DumyDest = Dest;
                        navMesh.ResetPath();
                        anim.CrossFade(ATTACK.name); // 발사
                    }
                    else
                    {
                        isMove = true;
                        isBaseAttack = true;
                        DumyDest = Dest;
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

            if (Enermy.GetComponent<Status>() && Enermy.GetComponent<Status>().HP <= 0)
            {
                Enermy = null;
                //isMove = false;
                //isAttack = false;
                if (DumyDest.x != 0 && DumyDest.z != 0)
                {
                    isIndicate = false;
                    disableIndicator();
                    isAttackCursor = false;
                    isMeteo = false;
                    isPowerMeteo = false;
                    ChaingeCursor(false);

                    Dest = DumyDest;
                    DumyDest = new Vector3(0, 0, 0);
                    isMove = true;
                    isAttack = false;
                    Enermy = null;
                    MoveOrder(Dest);
                    anim.CrossFade(MOVE.name);
                }
            }

            if (Enermy && !Enermy.activeSelf)
            {
                anim.CrossFade(IDLE.name);
                Enermy = null;

                if (DumyDest.x != 0 && DumyDest.z != 0)
                {
                    isIndicate = false;
                    disableIndicator();
                    isAttackCursor = false;
                    isMeteo = false;
                    isPowerMeteo = false;
                    ChaingeCursor(false);

                    Dest = DumyDest;
                    DumyDest = new Vector3(0, 0, 0);
                    isMove = true;
                    isAttack = false;
                    Enermy = null;
                    MoveOrder(Dest);
                    anim.CrossFade(MOVE.name);
                }
            }
        }
        else
        {
            if (isAttackDDang)
            {
                MaxDist = 9999;
                Collider[] colliders;
                colliders = Physics.OverlapSphere(this.transform.position, 3.0f);
                foreach (Collider col in colliders)
                {
                    if (col.gameObject.tag == "UndeadMinion" || col.gameObject.tag == "UndeadTower" || col.gameObject.tag == "Enermy")
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
                    isAttack = true;
                    isMove = false;
                    this.transform.LookAt(Enermy.transform);
                    anim.CrossFade(IDLE.name);
                    navMesh.ResetPath();
                }
            }

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
            isAttackDDang = false;
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
            print(hit.collider.gameObject.tag);
            if (hit.collider.gameObject.tag == "UndeadMinion" || hit.collider.gameObject.tag == "Tree" || hit.collider.gameObject.tag == "Enermy")
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
            if (hit.collider.gameObject.tag == "UndeadMinion" || hit.collider.gameObject.tag == "UndeadTower"|| hit.collider.gameObject.tag == "Tree" || hit.collider.gameObject.tag == "Enermy")
            {
                Enermy = hit.collider.gameObject;

                if (Enermy.GetComponent<Status>() && Enermy.GetComponent<Status>().HP < 0)
                    Enermy = null;

                if (Enermy)
                {
                    if (Vector3.Distance(this.transform.position, Enermy.transform.position) <= 3.0f)
                    {
                        isAttack = true;
                        isMove = false;
                        this.transform.LookAt(Enermy.transform);
                        anim.CrossFade(IDLE.name);
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
                //어택땅 혹은 무빙

                if (isAttackCursor) //어택땅
                {
                    isAttackDDang = true;
                }
                else
                {
                    isAttackDDang = false;
                }

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
