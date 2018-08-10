using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impail : MonoBehaviour
{
    private float ATK;
    private List<GameObject> MonsterColliderList;
    private float deadTime;
    private float fTime;

    public GameObject Master;

    private void Start()
    {
        MonsterColliderList = new List<GameObject>();
        fTime = 0;
        deadTime = this.transform.root.GetComponent<ParticleSystem>().main.duration;
    }

    private void Update()
    {
        fTime += Time.deltaTime;

        if (this.transform.root.GetComponent<ParticleSystem>().isStopped)//fTime > deadTime)
        {
            //foreach (GameObject go in test)
            //{
            //    if (go.GetComponent<Status>())
            //    {
            //        print(go.name);
            //    }
            //}
            fTime = 0;
            MonsterColliderList.Clear();
            Destroy(this.transform.root.gameObject);
            //this.transform.root.gameobject.setactive(false);
        }

    }

    private void OnParticleCollision(GameObject other)
    {
        if (!MonsterColliderList.Contains(other) && other.tag != "Player" && other.tag != "NaelTower" && other.tag != "UndeadTower" && other.tag != "NaelMinion")
        {
            if(other.GetComponent<Status>())
            {
                other.GetComponent<Status>().Marker = Master;
                other.GetComponent<Status>().HP -= 100.0f;
            }
            MonsterColliderList.Add(other);
        }
    }
}
