using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolItem : MonoBehaviour
{
    [HideInInspector] 
    public GameObject OriginPrefab;

    public void ReturnToPool()
    {
        PoolManager.Instance.ReturnToPool(gameObject);
    }
}
