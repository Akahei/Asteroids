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
    public Bounds LevelBounds;

    void Awake()
    {
        Instance = this;
        var viewportSize = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0)) * 2;
        viewportSize.z = 5;
        Size = viewportSize;
        Extents = Size / 2;
        LevelBounds = new Bounds(Vector3.zero, viewportSize);
    }

    void Update()
    {
        foreach (var actor in Actors)
        {
            if (!actor.gameObject.activeInHierarchy) continue;

            var objectPos = actor.gameObject.transform.position;
            if (!LevelBounds.Contains(actor.transform.position))
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

    public Vector2 GetRandomPointOnEdge()
    {
        float sideMultiplier = UE.Random.value > 0.5f ? 1f : -1f;
        float xEdgeWeight = Extents.x / (Extents.x + Extents.y);
        var randomPoint = new Vector2();
        if (xEdgeWeight < UE.Random.value)
        {
            randomPoint.x = Extents.x * sideMultiplier;
            randomPoint.y = UE.Random.Range(-Extents.y, Extents.y);
        }
        else
        {
            randomPoint.x = UE.Random.Range(-Extents.x, Extents.x);
            randomPoint.y = Extents.y * sideMultiplier;
        }
        return randomPoint;
    }

    public Vector2 GetRandomPointOnLeftRightEdge(float marginFromTopBot = 0)
    {
        var maxY = Extents.y * (1 - marginFromTopBot);
        return new Vector2(UE.Random.value > 0.5f ?  Extents.x : -Extents.x, UE.Random.Range(-maxY, maxY));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(LevelBounds.center, LevelBounds.size);
    }
}
