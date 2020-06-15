using UnityEngine;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(Actor))]
public class Ufo : MonoBehaviour
{
    public float Speed = 2;

    [Header("Weapon")]
    public Projectile ProjectilePrefab;
    public float MinFireCooldown = 2;
    public float MaxFireCooldown = 5;
    public Color ProjectileColor;

    float nextShootTime;
    Ship playerShip => GameManager.Instance.PlayerShip;

    void Start()
    {
        ScheduleNextFire();
        var rbody = GetComponent<Rigidbody>();   
        rbody.velocity = transform.up * Speed;
    }

    void ScheduleNextFire() => nextShootTime = Time.time + Random.Range(MinFireCooldown, MaxFireCooldown);

    void Update()
    {
        if (Time.time > nextShootTime) Fire();
    }

    void Fire()
    {
        if (playerShip)
        {
            var fireDirection = playerShip.transform.position -  transform.position;
            var projectile = PoolManager.Instance.GetInstance(ProjectilePrefab);
            projectile.transform.position = transform.position;
            projectile.transform.rotation = Quaternion.LookRotation(Vector3.forward, fireDirection);
            projectile.Init(gameObject, ProjectileColor);
        }
        ScheduleNextFire();
    }

}
