using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AkmaControl : MonoBehaviour
{
    private NavMeshAgent navMesh = null;


    private bool isMove = false;
    private bool isAttack = false; // 공격중인가?
    private bool isGoAttack = false; // 미니언 있으니 공격해도된다

    private Vector3 Dest;

    private void Awake()
    {
        navMesh = this.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(isMove) // 움직이고 있는가?
        {
            if(isGoAttack) //미니언하고 같이 움직이는건가?
            {
                if (SearchForwardMinion("UndeadMinion")) 
                {
                    Vector3 towerPos = GameObject.FindGameObjectWithTag("NaelTower").transform.position;
                    Dest = new Vector3(Random.Range(towerPos.x - 2, towerPos.x), towerPos.y, Random.Range(towerPos.z, towerPos.z - 2));
                    MoveToDestination(Dest);
                }
                else
                {
                    Vector3 towerPos = GameObject.FindGameObjectWithTag("UndeadTower").transform.position;
                    Dest = new Vector3(Random.Range(towerPos.x - 2, towerPos.x), towerPos.y, Random.Range(towerPos.z, towerPos.z - 2));
                    MoveToDestination(Dest);
                }

                if(SearchGameObject("NaelMinion") || SearchGameObject("NaelTower") || SearchGameObject("Player")) //공격대상을 넣어준다
                {
                    isMove = false;
                    isAttack = true;
                    navMesh.ResetPath();
                }
            }

            if (Vector3.Distance(this.transform.position, Dest) < 0.1f) // 목적지에 도착했는가?
            {
                isMove = false;
                isAttack = false;
                this.transform.position = Dest;
                navMesh.ResetPath();
            }
        }
        else
        {
            if(isAttack)
            {
                if(SearchGameObject("UndeadMinion")) //내주변에 미니언이 있는가?
                {
                    //스킬있는대로 다퍼붓자
                    //if Enermy가 죽었다면 다시 앞으로 가며 탐색
                   // if()
                }
                else // 내가 너무 앞에 나와있거나 미니언이없다.
                {
                   
                    //졸렬하게 도망가자
                    isGoAttack = false;
                    isMove = true;
                    Vector3 towerPos = GameObject.FindGameObjectWithTag("UndeadTower").transform.position;
                    Dest = new Vector3(Random.Range(towerPos.x - 2, towerPos.x), towerPos.y, Random.Range(towerPos.z, towerPos.z - 2));
                    MoveToDestination(Dest);
                }
            }
            else
            {
                if(SearchGameObject("UndeadTower")) // 타워 근처이다
                {
                    if(SearchForwardMinion("UndeadMinion")) // 내 주변에 미니언이 있는가?
                    {
                        //공격하러 가즈아
                        isMove = true;
                        isGoAttack = true;
                        Vector3 towerPos = GameObject.FindGameObjectWithTag("NaelTower").transform.position;
                        Dest = new Vector3(Random.Range(towerPos.x - 2, towerPos.x), towerPos.y, Random.Range(towerPos.z, towerPos.z - 2));
                        MoveToDestination(Dest);
                    }
                    else //없다
                    {
                        //대기
                    }
                }
                else // 움직임도없고 공격중이지도 않으며 타워주변이 아니므로 타워로 돌아간다.
                {
                    isMove = true;
                    Vector3 towerPos = GameObject.FindGameObjectWithTag("UndeadTower").transform.position;
                    Dest = new Vector3(Random.Range(towerPos.x - 2, towerPos.x), towerPos.y, Random.Range(towerPos.z, towerPos.z - 2));
                    MoveToDestination(Dest);
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
}
