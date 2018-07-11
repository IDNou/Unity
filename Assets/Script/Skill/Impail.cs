using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impail : MonoBehaviour
{
    private float ATK;
    private List<GameObject> test;
    private float deadTime;
    private float fTime;
    
    private void Start()
    {
        test = new List<GameObject>();
        fTime = 0;
        deadTime = this.transform.root.GetComponent<ParticleSystem>().duration;
    }

    private void Update()
    {
        fTime += Time.deltaTime;

        if (fTime > deadTime)
        {
            //foreach (GameObject go in test)
            //{
            //    if (go.GetComponent<Status>())
            //    {
            //        print(go.name);
            //    }
            //}
            fTime = 0;
            test.Clear();
            //this.transform.root.gameobject.setactive(false);
        }

    }

    private void OnParticleCollision(GameObject other)
    {
        if (!test.Contains(other))
        {
            print(other.name);
            test.Add(other);
        }
    }
}
