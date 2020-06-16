using System;
using UnityEngine;

public class Actor : MonoBehaviour
{
    Bounds LevelBounds => LevelBox.Instance.LevelBounds;
    Vector2 LevelSize => LevelBox.Instance.Size;
    Vector2 LevelExtents => LevelBox.Instance.Extents;

    private void Update()
    {
        if (!LevelBounds.Contains(transform.position))
        {
            var newPosition = gameObject.transform.position;
            var currentPosition = gameObject.transform.position;
            if (Math.Abs(currentPosition.x) > LevelExtents.x)
            {
                newPosition.x -= LevelSize.x * Math.Sign(currentPosition.x);
            }

            if (Math.Abs(currentPosition.y) > LevelExtents.y)
            {
                 newPosition.y -= LevelSize.y * Math.Sign(currentPosition.y);
            }
            gameObject.transform.position = newPosition;
        }
    }
}
