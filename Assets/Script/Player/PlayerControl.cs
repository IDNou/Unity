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

    private Texture2D attackCursorTexture;
    private Texture2D mouseCursorTexture;
    private Vector2 hotSpot;


    private bool isAttackCursor = false;
    private bool isAttack = false;
    private bool isMove = false;
    private bool isCharge = true;

    private bool isMeteo = false;
    private bool isRainOfFire = false;
    private bool isPowerMeteo = false;

    private float animDelay = 0;

    private void Awake()
    {
        navMesh = this.GetComponent<NavMeshAgent>();
        anim = this.GetComponentInChildren<Animation>();
        goIndicator.SetActive(false);
        attackCursorTexture = Resources.Load<Texture2D>("AttackCursor");
        mouseCursorTexture = Resources.Load<Texture2D>("MouseCursor");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            RayCastingToDiscriminate();
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
                // 먼저 레이캐스팅을 해서 적이 타게팅 됬는지 파악해야함
                isMeteo = false;
                ChaingeCursor(false);
                anim.CrossFade(SKILL1.name);
                navMesh.ResetPath();
            }
            else if(isRainOfFire)
            {
                isRainOfFire = false;
                ChaingeCursor(false);
                disableIndicator();
                anim.CrossFade(SKILL1.name);
                navMesh.ResetPath();
            }
            else if(isPowerMeteo)
            {
                isPowerMeteo = false;
                ChaingeCursor(false);
                disableIndicator();
                anim.CrossFade(SKILL1.name);
                navMesh.ResetPath();
            }
            else
            {
                //적 스테이터스창 보여주게해야함
            }
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            isAttackCursor = true;
            ChaingeCursor(true);
        }
        else if(Input.GetKeyDown(KeyCode.T)) //메테오
        {
            isMeteo = true;
            ChaingeCursor(true);
        }
        else if(Input.GetKeyDown(KeyCode.F)) // 레인파이어
        {
            isRainOfFire = true;
           
        }
        else if(Input.GetKeyDown(KeyCode.W)) // 팜
        {
            isPowerMeteo = true;
        }

        if (Enermy)
        {
            if (Vector3.Distance(this.transform.position, Enermy.transform.position) < 3.0f)
            {
                isAttack = true;
                this.transform.LookAt(Enermy.transform);
                navMesh.ResetPath();
            }
            else
            {
                isMove = true;
                Dest = Enermy.transform.position;
                MoveOrder(Dest);
                anim.CrossFade(MOVE.name);
            }

            if (isMove)
            {
                Dest = Enermy.transform.position;
                MoveOrder(Dest);

                if (Vector3.Distance(this.transform.position, Enermy.transform.position) < 3.0f)
                {
                    isMove = false;
                    isAttack = true;
                    this.transform.LookAt(Enermy.transform);
                    navMesh.ResetPath();
                }
            }

            if (isAttack)
            {
                if (isCharge)
                {
                    isCharge = false;
                    animDelay = ATTACK.length * 2.0f;
                    anim.CrossFade(ATTACK.name);
                }
            }

            if (!Enermy.activeSelf)
            {
                anim.CrossFade(IDLE.name);
                Enermy = null;
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

    private void LateUpdate()
    {
        if (isRainOfFire || isPowerMeteo)
        {
            enabledIndicator();
        }
    }

    private void RayCastingToDiscriminate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            print(hit.collider.name);
            if (hit.collider.gameObject.tag == "UndeadMinion")//|| hit.collider.gameObject.tag == "Tree")
            {
                ChaingeCursor(true);

                 Enermy = hit.collider.gameObject;

                if (Enermy.GetComponent<Status>() && Enermy.GetComponent<Status>().nHP < 0)
                    Enermy = null;

                if (Enermy)
                {
                    if (Vector3.Distance(this.transform.position, Enermy.transform.position) < 3.0f)
                    {
                        isAttack = true;
                        this.transform.LookAt(Enermy.transform);
                        navMesh.ResetPath();
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
                Dest = hit.point;
                isMove = true;
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
