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
    public AnimationClip IDLE;
    public AnimationClip ATTACK;
    public AnimationClip MOVE;
    public AnimationClip SKILL1;
    public AnimationClip DEATH;
    public AnimationClip STURN;


    private bool isAttack = false;
    private bool isMove = false;
    private bool isChage = true;

    private float animDelay = 0;

    private void Awake()
    {
        navMesh = this.GetComponent<NavMeshAgent>();
        anim = this.GetComponentInChildren<Animation>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(Input.mousePosition, ray.direction);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                print(hit.collider.gameObject.tag);

                if (hit.collider.gameObject.tag == "UndeadMinion" || hit.collider.gameObject.tag == "Tree")
                {
                    Enermy = hit.collider.gameObject;

                    if (Enermy.GetComponent<Status>().nHP < 0)
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
        else if(Input.GetKeyDown(KeyCode.A))
        {
            // 커서 바뀌어야함
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
                if (isChage)
                {
                    isChage = false;
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
            isChage = true;
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
