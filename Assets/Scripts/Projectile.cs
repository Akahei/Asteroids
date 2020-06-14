using UnityEngine;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(Actor))]
public class Projectile : MonoBehaviour
{
    public float LifeTime = 5;
    public float Speed;

    public GameObject Owner { get; private set; }

    GameObject owner;
    Rigidbody rbody;
    PoolItem poolItem;
    float destroyTime;
    Vector3 velocity;

    void Awake()
    {
        poolItem = GetComponent<PoolItem>();
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
            Destroy();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        var destructible = other.GetComponentInParent<Destructible>();
        if (destructible == null || destructible.gameObject == owner) return;
        destructible.Destroy();
        Destroy();
    }

    void Destroy()
    {
        if (poolItem != null) poolItem.ReturnToPool();
    }
}
