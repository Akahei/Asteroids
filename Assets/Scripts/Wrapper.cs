using System;
using System.Collections.Generic;
using UnityEngine;

public class Wrapper : MonoBehaviour
{
    static public Wrapper Instance { get; private set; }

    public List<Actor> Actors { get; private set; } = new List<Actor>();
    private BoxCollider boxCollider;
    private Vector2 extents;


    private void Awake()
    {
        Instance = this;
        boxCollider = GetComponent<BoxCollider>();
        extents = boxCollider.size;
    }

    void Update()
    {
        foreach (var actor in Actors)
        {
            if (!actor.gameObject.activeInHierarchy) continue;

            var objectPos = actor.gameObject.transform.position;
            if (!boxCollider.bounds.Contains(actor.transform.position))
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
                actor.gameObject.transform.position = objectPos;
            }
        }
    }
}
