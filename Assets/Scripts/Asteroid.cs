using UnityEngine;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(Actor))]
public class Asteroid : MonoBehaviour
{
    public float minSpeed = 1;
    public float maxSpeed = 10;

    Rigidbody rbody;

    void Awake()
    {
        rbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        transform.rotation = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward);
        rbody.velocity = transform.up * Random.Range(minSpeed, maxSpeed);
    }
}
