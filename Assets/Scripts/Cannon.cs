using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Projectile ProjectilePrefab;
    public Color ProjectileColor;

    AudioSource shootSFX;
    MaterialPropertyBlock projectileColorMPB;

    void Awake()
    {
        shootSFX = GetComponent<AudioSource>();
    }

    void Start()
    {
        projectileColorMPB = new MaterialPropertyBlock();
        projectileColorMPB.SetColor("_Color", ProjectileColor);
    }

    public void Fire(GameObject owner)
    {
        var projectile = PoolManager.Instance.GetInstance(ProjectilePrefab);
        projectile.transform.position = transform.position;
        projectile.transform.rotation = transform.rotation;
        projectile.Init(owner, projectileColorMPB);
        if (shootSFX) shootSFX.Play();
    }
}
