using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Fireball : MonoBehaviour
{
    public bool pushOnAwake = true;
    public Vector3 startDirection;
    public Vector3 Destination;
    public float fSpeed = 8.0f;
    public float ATK;
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
            Push(startDirection);
        }
    }

    private void Update()
    {
        Push(startDirection);
    }

    public void Push(Vector3 direction)
    {
        Vector3 dir = direction.normalized;
        this.transform.Translate(dir * fSpeed * Time.deltaTime);
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

        Vector3 vecBomPos = this.transform.position;
        vecBomPos.y = 0;

        Collider[] colliders;
        colliders = Physics.OverlapSphere(Destination, this.GetComponent<SphereCollider>().radius);
        foreach (Collider coll in colliders)
        {
            if(coll.tag == "UndeadMinion")
            {
                coll.gameObject.GetComponent<Status>().nHP -= ATK;
            }
        }

        this.GetComponent<SphereCollider>().enabled = false;
    }

    public void StopParticleSystem(GameObject g)
    {
        ParticleSystem[] par;
        par = g.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem p in par)
        {
            p.Stop();
        }

        Destroy(this.gameObject, explodeEffect.GetComponent<ParticleSystem>().main.duration);
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


