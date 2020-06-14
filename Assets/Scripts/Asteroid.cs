﻿using UnityEngine;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(Actor))]
public class Asteroid : MonoBehaviour
{
    [Header("Speed")]
    public float minSpeed = 1;
    public float maxSpeed = 10;
    [Header("Smaller asteroids")]
    public Asteroid SmallerAsteroidPrefab;
    public int SmallerAsterodsNum = 2;
    public float AngleBetweenSmallerAsteroids = 90;

    Rigidbody rbody;

    void Awake()
    {
        rbody = GetComponent<Rigidbody>();
        var destructible = GetComponent<Destructible>();
        destructible.OnDestory += OnDestructibleDestory;
    }

    public void Init(Vector2 pos, float rot, float speed)
    {
        transform.position = pos;
        transform.rotation = Quaternion.Euler(0, 0, rot);
        rbody.velocity = transform.up * speed;
        GameManager.Instance.RegisterAsteroid(this);
    }

    public float GetRandomSpeed() => Random.Range(minSpeed, maxSpeed);

    void OnDestructibleDestory()
    {
        if (SmallerAsteroidPrefab != null)
        {
            float speed = SmallerAsteroidPrefab.GetRandomSpeed();
            float myAngle = transform.rotation.eulerAngles.z;
            float fullArc = AngleBetweenSmallerAsteroids * (SmallerAsterodsNum - 1);
            float angle = myAngle - fullArc / 2;
            for (int i = 0; i < SmallerAsterodsNum; i++, angle += AngleBetweenSmallerAsteroids)
            {
                var asteroid = PoolManager.PMInstance.GetInstance(SmallerAsteroidPrefab);
                asteroid.Init(transform.position, angle, speed);
            }
        }
    }
}
