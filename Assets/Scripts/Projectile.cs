using UnityEngine;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(Actor))]
public class Projectile : MonoBehaviour
{
    public float LifeTime = 5;
    public float Speed;

    public GameObject Owner { get; private set; }

    GameObject owner;
    Rigidbody rbody;
    Actor actor;
    float destroyTime;
    Vector3 velocity;

    void Awake()
    {
        actor = GetComponent<Actor>();
        rbody = GetComponent<Rigidbody>();
    }

    public void Init(GameObject projectileOwner)
    {
        owner = projectileOwner;
        destroyTime = Time.time + LifeTime;
        rbody.velocity = transform.up * Speed;
    }

    void Update()
    {
        if (Time.time >= destroyTime)
        {
            actor.Die();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        var actor = other.GetComponentInParent<Actor>();
        if (actor == null) return;

        if (actor.gameObject == owner) return;
        actor.Die();

        var pObject = GetComponent<PoolObject>();
        if (pObject)
        {
            pObject.ReturnToPool();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
}
