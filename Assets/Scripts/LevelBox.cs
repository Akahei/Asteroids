using System;
using System.Collections.Generic;
using UnityEngine;
using UE = UnityEngine;

public class LevelBox : MonoBehaviour
{
    static public LevelBox Instance { get; private set; }

    public List<Actor> Actors { get; private set; } = new List<Actor>();
    public Vector2 Size { get; private set; }
    public Vector2 Extents { get; private set; }
    private BoxCollider boxCollider;

    void Awake()
    {
        Instance = this;
        boxCollider = GetComponent<BoxCollider>();
        Size = boxCollider.size;
        Extents = boxCollider.size / 2;
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
                if (Math.Abs(relativePos.x) > Size.x / 2)
                {
                    if (relativePos.x > 0)
                    {
                        objectPos.x -= Size.x;
                    }
                    else
                    {
                        objectPos.x += Size.x;
                    }
                }

                if (Math.Abs(relativePos.y) > Size.y / 2)
                {
                    if (relativePos.y > 0)
                    {
                        objectPos.y -= Size.y;
                    }
                    else
                    {
                        objectPos.y += Size.y;
                    }
                }
                actor.gameObject.transform.position = objectPos;
            }
        }
    }

    public Vector2 GetRandomPointOnEdge(float margin = 0)
    {
        var margedExtents = Extents;
        margedExtents.x -= margin;
        margedExtents.y -= margin;
        float sideMultiplier = UE.Random.value > 0.5f ? 1f : -1f;
        float xWeight = margedExtents.x / (margedExtents.x + margedExtents.y);
        var randomPoint = new Vector2();
        if (xWeight < UE.Random.value)
        {
            randomPoint.x = margedExtents.x * sideMultiplier;
            randomPoint.y = UE.Random.Range(-margedExtents.y, margedExtents.y);
        }
        else
        {
            randomPoint.x = UE.Random.Range(-margedExtents.x, margedExtents.x);
            randomPoint.y = margedExtents.y * sideMultiplier;
        }
        return randomPoint;
    }
}
