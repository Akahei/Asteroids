using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public float LifeTime = 5;
    public float Speed;

    public GameObject Owner { get; private set; }

    Rigidbody rbody;
    float destroyTime;
    Vector3 velocity;

    void Awake()
    {
        rbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rbody.velocity = transform.up * Speed;
        destroyTime = Time.time + LifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= destroyTime)
        {
            Destroy(gameObject);
        }
    }
}
