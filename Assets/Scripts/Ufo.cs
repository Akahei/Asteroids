using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ufo : Destructible
{
    [Header("Movement")]
    public float Speed = 2;
    public float Amplitude = 1.5f;
    public float Preiod = 3;

    [Header("Weapon")]
    public Cannon UfoCannon;
    public float MinFireCooldown = 2;
    public float MaxFireCooldown = 5;

    float nextShootTime;
    float nextTimeChangeDirection;
    Ship playerShip => GameManager.Instance.PlayerShip;
    Rigidbody rbody;

    void Start()
    {
        ScheduleNextFire();
        rbody = GetComponent<Rigidbody>();   
    }

    void ScheduleNextFire() => nextShootTime = Time.time + Random.Range(MinFireCooldown, MaxFireCooldown);

    void Update()
    {
        var sinMovement = Mathf.Sin(Time.time * Mathf.PI * 2 / Preiod) * Amplitude;
        rbody.velocity = (transform.up + transform.right * sinMovement) * Speed;

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
