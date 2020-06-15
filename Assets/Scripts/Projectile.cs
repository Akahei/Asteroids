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

    public void Init(GameObject projectileOwner, MaterialPropertyBlock materialPropBlock = null)
    {
        owner = projectileOwner;
        destroyTime = Time.time + LifeTime;
        rbody.velocity = transform.up * Speed;

        foreach (var rend in GetComponentsInChildren<Renderer>())
        {
            rend.SetPropertyBlock(materialPropBlock);
        }
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
        var asteroid = other.GetComponentInParent<Asteroid>();
        if (asteroid != null) asteroid.Break();
        
        var destructible = other.GetComponentInParent<Destructible>();
        if (destructible == null || destructible.gameObject == owner) return;
        destructible.Destroy();
        Destroy();
    }

    void Destroy()
    {
        if (poolItem != null)
        {
            poolItem.ReturnToPool();
        } 
        else
        {
            Destroy(gameObject);
        }
    }
}
