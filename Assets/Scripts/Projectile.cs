using UnityEngine;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(PoolItem))]
public class Projectile : MonoBehaviour, IResetable
{
    public float LifeTime = 5;
    public float Speed;

    public GameObject Owner { get; private set; }

    GameObject owner;
    bool ownerIsPlayer = false;
    PoolItem poolItem;
    Rigidbody rbody;
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
        ownerIsPlayer = GameManager.Instance.PlayerShip.gameObject == projectileOwner;
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

    public void ResetObject()
    {
        Destroy();
    }

    void Destroy() => poolItem.ReturnToPool();
    
    void OnTriggerEnter(Collider other)
    {
        var otherDest = other.GetComponentInParent<Destructible>();
        if (otherDest != null) 
        {
            var asteroid = otherDest as Asteroid;
            if (asteroid != null)
            {
                asteroid.Break();
            }
            if (owner != otherDest.gameObject)
            {
                otherDest.Explode();
                Destroy();
            }
        }
    }
}
