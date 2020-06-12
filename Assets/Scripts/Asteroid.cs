using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float minSpeed = 1;
    public float maxSpeed = 10;

    public Vector3 velocity;

    void Start()
    {
        transform.rotation = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward);
        velocity = transform.up * Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        transform.position += velocity * Time.deltaTime;
    }
}
