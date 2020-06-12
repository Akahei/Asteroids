using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrapper : MonoBehaviour
{
    public List<Wrappable> Wrappables { get; private set; }
    private BoxCollider boxCollider;
    private Vector2 extents;


    private void Awake()
    {
        Wrappables = new List<Wrappable>();
        boxCollider = GetComponent<BoxCollider>();
        extents = boxCollider.size;
    }

    void Start()
    {
    }

    void Update()
    {
        foreach (var wrappable in Wrappables)
        {
            var objectPos = wrappable.gameObject.transform.position;
            if (!boxCollider.bounds.Contains(wrappable.transform.position))
            {
                var relativePos = objectPos - gameObject.transform.position;
                if (Math.Abs(relativePos.x) > extents.x / 2)
                {
                    if (relativePos.x > 0)
                    {
                        objectPos.x -= extents.x;
                    }
                    else
                    {
                        objectPos.x += extents.x;
                    }
                }

                if (Math.Abs(relativePos.y) > extents.y / 2)
                {
                    if (relativePos.y > 0)
                    {
                        objectPos.y -= extents.y;
                    }
                    else
                    {
                        objectPos.y += extents.y;
                    }
                }
                wrappable.gameObject.transform.position = objectPos;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        
    }
}
