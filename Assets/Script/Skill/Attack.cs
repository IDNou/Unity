using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Attack : MonoBehaviour
{
    public bool pushOnAwake = true;
    public Vector3 startDirection;
    public float fSpeed;
    public ForceMode forceMode;
    public GameObject Enermy;
    public GameObject Master;

    public GameObject fieryEffect;
    public GameObject smokeEffect;
    public GameObject explodeEffect;

    protected Rigidbody rgbd;
    public float ATK;

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
        if (Enermy)
        {
            Vector3 dir = Enermy.transform.position - this.transform.position;
            dir.Normalize();
            Push(dir);

            if (Vector3.Distance(this.transform.position, Enermy.transform.position) < 0.3f)
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

                if (Enermy.GetComponent<Status>())
                {
                    Enermy.GetComponent<Status>().Marker = Master;
                    Enermy.GetComponent<Status>().HP -= ATK;
                }
                Destroy(this.gameObject);
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void Push(Vector3 direction)
    {
        Vector3 dir = direction.normalized;
        this.transform.Translate(dir * fSpeed * Time.deltaTime);
    }

    public void StopParticleSystem(GameObject g)
    {
        ParticleSystem[] par;
        par = g.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem p in par)
        {
            p.Stop();
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
}


