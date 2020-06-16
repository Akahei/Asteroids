using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ufo : Destructible
{
    public float Speed = 2;

    [Header("Weapon")]
    public Projectile ProjectilePrefab;
    public float MinFireCooldown = 2;
    public float MaxFireCooldown = 5;
    public Color ProjectileColor;

    float nextShootTime;
    Ship playerShip => GameManager.Instance.PlayerShip;
    MaterialPropertyBlock projectileColorMPB;

    void Start()
    {
        ScheduleNextFire();
        var rbody = GetComponent<Rigidbody>();   
        rbody.velocity = transform.up * Speed;
        projectileColorMPB = new MaterialPropertyBlock();
        projectileColorMPB.SetColor("_Color", ProjectileColor);
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
            projectile.Init(gameObject, projectileColorMPB);
        }
        ScheduleNextFire();
    }

}
