using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Status))]
public class MinionContol : MonoBehaviour
{
    public GameObject Dest;
    public GameObject Enermy = null;
    //private GameObject[] Enermys;
    private NavMeshAgent NavMesh;
    private float MaxDist = 9999;

    private Animator anim;

    private void Awake()
    {
        Dest = GameObject.Find("TurnPoint");
        NavMesh = this.GetComponent<NavMeshAgent>();
        SetDest(Dest.transform.position);

        anim = this.GetComponentInChildren<Animator>();
        anim.SetFloat("MoveSpeed", NavMesh.speed);
    }

    private void Update()
    {
        //적탐색
        MaxDist = 9999;
        Collider[] colliders;
        colliders = Physics.OverlapSphere(this.transform.position, this.GetComponent<SphereCollider>().radius);
        foreach(Collider col in colliders)
        {
            if(this.gameObject.tag == "NaelMinion")
            {
                if(col.gameObject.tag == "UndeadMinion" || col.gameObject.tag == "UndeadTower")
                {
                    if(Vector3.Distance(this.transform.position,col.transform.position) < MaxDist)
                    {
                        Enermy = col.gameObject;
                        MaxDist = Vector3.Distance(this.transform.position, col.transform.position);
                    }
                }
            }
            else
            {
                if(col.gameObject.tag == "NaelMinion" || col.gameObject.tag == "NaelTower")
                {
                    if (Vector3.Distance(this.transform.position, col.transform.position) < MaxDist)
                    {
                        Enermy = col.gameObject;
                        MaxDist = Vector3.Distance(this.transform.position, col.transform.position);
                    }
                }
            }
        }

        if (Enermy)
        {
            SetDest(Enermy.transform.position);
            anim.SetFloat("MoveSpeed", NavMesh.speed);
        }
        
        // 목적지 이동
        if (Vector3.Distance(this.transform.position, Dest.transform.position) < 0.3f)
        {
            if (Dest.name == "TurnPoint")
            {
                if (this.gameObject.tag == "NaelMinion")
                {
                    Dest = GameObject.Find("UndeadPoint");
                    SetDest(Dest.transform.position);
                    anim.SetFloat("MoveSpeed", NavMesh.speed);
                }
                else
                {
                    Dest = GameObject.Find("NaelPoint");
                    SetDest(Dest.transform.position);
                    anim.SetFloat("MoveSpeed", NavMesh.speed);
                }
            }
            else
            {
                anim.SetFloat("MoveSpeed", 0);
            }
        }

        if (Enermy)
        {
            this.transform.LookAt(Enermy.transform);
            if (Vector3.Distance(this.transform.position, Enermy.transform.position) < 0.9f)
            {
                NavMesh.isStopped = true;
                anim.SetBool("Attack", true);
            }
            else
            {
                NavMesh.isStopped = false;
                anim.SetBool("Attack", false);
            }

            if (Enermy.activeSelf == false)
            {
                Enermy = null;
                anim.SetBool("Attack", false);
                SetDest(Dest.transform.position);
                NavMesh.isStopped = false;
            }
        }
        else
        {
            SetDest(Dest.transform.position);
        }

    }

    private void OnDrawGizmos()
    {
        if (NavMesh)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(this.transform.position, this.transform.position + NavMesh.velocity);

            for (int i = 0; i < NavMesh.path.corners.Length; ++i)
            {
                Gizmos.DrawWireSphere(NavMesh.path.corners[i], 0.5f);
            }
        }
    }

    public void SetDest(Vector3 DestTo)
    {
        NavMesh.SetDestination(DestTo);
    }
}
