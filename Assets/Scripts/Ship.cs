using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public float MaxSpeed = 10;
    public float Acceleration = 1;
    public float RotateSpeed = 100;

    public Vector3 velocity = new Vector3();

    void Start()
    {
        
    }

    void Update()
    {
        transform.position += velocity * Time.deltaTime;
    }

    public void Accelerate()
    {
        velocity = Vector3.ClampMagnitude(velocity + transform.up * Acceleration * Time.deltaTime, MaxSpeed);
    }

    public void Rotate(float direction)
    {
        transform.Rotate(0, 0, RotateSpeed * direction * Time.deltaTime);
    }

    public void RotateTowards(Vector3 target)
    {
        var dest = target - transform.position;
        dest.z = 0;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Vector3.forward, dest), RotateSpeed * Time.deltaTime);
    }

}
