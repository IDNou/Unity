using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Fireball : MonoBehaviour
{
    public bool pushOnAwake = true;
    public Vector3 startDirection;
    public Vector3 Destination;
    public float startMagnitude;
    public ForceMode forceMode;

    public GameObject fieryEffect;
    public GameObject smokeEffect;
    public GameObject explodeEffect;

    protected Rigidbody rgbd;

    public void Awake()
    {
        rgbd = GetComponent<Rigidbody>();
    }

    public void Start()
    {
        if (pushOnAwake)
        {
            Push(startDirection, startMagnitude);
        }
    }

    private void Update()
    {
        Push(startDirection, startMagnitude);
    }

    public void Push(Vector3 direction, float magnitude)
    {
        //Vector3 dir = direction.normalized;
        //rgbd.AddForce(dir * magnitude, forceMode);
        this.transform.Translate(direction * 5.0f * Time.deltaTime);
    }

    public void OnCollisionEnter(Collision col)
    {
        rgbd.Sleep();
        if (fieryEffect != null)
        {
            StopParticleSystem(fieryEffect);
        }
        if (smokeEffect != null)
        {
            StopParticleSystem(smokeEffect);
        }
        if (explodeEffect != null)
            explodeEffect.SetActive(true);
    }

    public void StopParticleSystem(GameObject g)
    {
        ParticleSystem[] par;
        par = g.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem p in par)
        {
            p.Stop();
            //Destroy(this.gameObject);
        }
    }

    public void OnEnable()
    {
        if (fieryEffect != null)
            fieryEffect.SetActive(true);
        if (smokeEffect != null)
            smokeEffect.SetActive(true);
        if (explodeEffect != null)
            explodeEffect.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position, Destination);
    }
}


