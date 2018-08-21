using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FarMinionAnim : MonoBehaviour
{
    private Animation anim;
    public AnimationClip Idle;
    public AnimationClip Attack;
    public AnimationClip Move;
    public AnimationClip Die;

    public GameObject Dest;
    public GameObject Enermy = null;

    private NavMeshAgent navMesh;
    private float MaxDist = 9999;

    private void Awake()
    {
        navMesh = this.GetComponent<NavMeshAgent>();
        anim = this.GetComponentInChildren<Animation>();

        Dest = GameObject.Find("TurnPoint");
        SetDest(Dest.transform.position);
        //print(this.gameObject.name + Dest.transform.position);
    }

    private void Update()
    {
        //적탐색
        MaxDist = 9999;
        Collider[] colliders;
        colliders = Physics.OverlapSphere(this.transform.position, this.GetComponent<SphereCollider>().radius);
        foreach (Collider col in colliders)
        {
            if (this.gameObject.tag == "NaelMinion")
            {
                if (col.gameObject.tag == "UndeadMinion" || col.gameObject.tag == "UndeadTower")
                {
                    if (Vector3.Distance(this.transform.position, col.transform.position) < MaxDist)
                    {
                        Enermy = col.gameObject;
                        MaxDist = Vector3.Distance(this.transform.position, col.transform.position);
                    }
                }
            }
            else
            {
                if (col.gameObject.tag == "NaelMinion" || col.gameObject.tag == "NaelTower" || col.gameObject.tag == "Player")
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
            this.transform.LookAt(Enermy.transform);

            if (Vector3.Distance(this.transform.position, Enermy.transform.position) < 4.0f)
            {
                navMesh.isStopped = true;
                anim.CrossFade(Attack.name);
            }
            else
            {
                navMesh.isStopped = false;
                anim.CrossFade(Move.name);
            }

            if (Enermy.activeSelf == false || Enermy.GetComponent<Status>().HP <= 0)
            {
                Enermy = null;
                SetDest(Dest.transform.position);
                navMesh.isStopped = false;
            }
        }
        else
        {
            if (Vector3.Distance(this.transform.position, Dest.transform.position) < 0.3f)
            {
                if (Dest.name == "TurnPoint")
                {
                    if (this.gameObject.tag == "NaelMinion")
                    {
                        Dest = GameObject.Find("UndeadPoint");
                        SetDest(Dest.transform.position);
                    }
                    else
                    {
                        Dest = GameObject.Find("NaelPoint");
                        SetDest(Dest.transform.position);
                    }
                }
                else
                {
                    anim.CrossFade(Idle.name);
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
                Gizmos.DrawWireSphere(navMesh.path.corners[i], 0.5f);
            }
        }
    }

    public void SetDest(Vector3 DestTo)
    {
        navMesh.SetDestination(DestTo);
        anim.CrossFade(Move.name);
    }
}