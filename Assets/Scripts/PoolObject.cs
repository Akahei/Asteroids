using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    [HideInInspector] 
    public GameObject OriginPrefab;

    public void ReturnToPool()
    {
        PoolManager.Instance.ReturnToPool(gameObject);
    }
}
