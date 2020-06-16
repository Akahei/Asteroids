﻿using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ship : Destructible
{
    public float SpawnInvulnerableTime = 3;

    [Header("Movement")]
    public float MaxSpeed = 10;
    public float Acceleration = 1;
    public float RotateSpeed = 100;

    [Header("Weapon")]
    public Projectile ProjectilePrefab;
    public Transform FirePoint;
    public float MaxProjectilePerSec = 10;
    public Color ProjectileColor;
    
    Rigidbody rbody;
    BlinkAnimation blinkAnimation;
    float fireCooldown = 0;
    bool isInvulnerable = false;
    float invulnerableTimeEnd = 0;
    MaterialPropertyBlock projectileColorMPB;

    protected override void Awake()
    {
        base.Awake();
        rbody = GetComponent<Rigidbody>();
        blinkAnimation = GetComponent<BlinkAnimation>();
    }

    void Start()
    {
        isInvulnerable = true;
        invulnerableTimeEnd = Time.time + SpawnInvulnerableTime;
        SetEnableCollision(false);
        if (blinkAnimation) blinkAnimation.enabled = true;

        projectileColorMPB = new MaterialPropertyBlock();
        projectileColorMPB.SetColor("_Color", ProjectileColor);
    }

    void Update()
    {
        if (isInvulnerable && Time.time > invulnerableTimeEnd)
        {
            isInvulnerable = false;
            SetEnableCollision(true);
            if (blinkAnimation) blinkAnimation.enabled = false;
        }
    }

    private void SetEnableCollision(bool enable)
    {
        foreach(var collider in GetComponentsInChildren<Collider>())
        {
            collider.enabled = enable;
        }
    }

    public void Fire()
    {
        if (Time.time > fireCooldown)
        {
            fireCooldown = Time.time + 1f / MaxProjectilePerSec;
            var projectile = PoolManager.Instance.GetInstance(ProjectilePrefab);
            projectile.transform.position = FirePoint.position;
            projectile.transform.rotation = FirePoint.rotation;
            projectile.Init(gameObject, projectileColorMPB);
        }
    }

    public void Accelerate()
    {
        rbody.velocity = Vector3.ClampMagnitude(rbody.velocity + transform.up * Acceleration * Time.deltaTime, MaxSpeed);
    }

    public void Rotate(float direction)
    {
        var delta = Quaternion.Euler(0, 0, RotateSpeed * direction * Time.deltaTime);
        rbody.MoveRotation(rbody.rotation * delta);
    }

    public void RotateTowards(Vector3 target)
    {
        var dest = target - transform.position;
        dest.z = 0;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Vector3.forward, dest), RotateSpeed * Time.deltaTime);
    }

}
