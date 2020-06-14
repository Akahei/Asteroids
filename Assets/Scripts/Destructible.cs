using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    PoolItem poolItem;
    void Start()
    {
        poolItem = GetComponent<PoolItem>();
    }

    public void Destroy()
    {
        if (poolItem)
        {
            poolItem.ReturnToPool();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
