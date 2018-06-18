using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impail : MonoBehaviour
{
    private float ATK;

    private void OnParticleCollision(GameObject other)
    {
        print(other.name);
    }
}
