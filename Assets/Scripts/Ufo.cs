using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ufo : Destructible
{
    public float Speed = 2;

    [Header("Weapon")]
    public Cannon UfoCannon;
    public float MinFireCooldown = 2;
    public float MaxFireCooldown = 5;

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
            UfoCannon.transform.rotation = Quaternion.LookRotation(Vector3.forward, fireDirection);
            UfoCannon.Fire(gameObject);
        }
        ScheduleNextFire();
    }

}
