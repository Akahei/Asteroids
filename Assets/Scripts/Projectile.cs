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
        if (other.GetComponentInParent<Projectile>() != null) return;

        var otherActor = other.GetComponentInParent<Actor>();
        if (otherActor == null || otherActor.gameObject == owner) return;
        otherActor.Die();

        actor.Die();
    }
}
