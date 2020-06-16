using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolItem : MonoBehaviour
{
    [HideInInspector] 
    public Object OriginPrefab;
    [HideInInspector]
    public Object InstancedObject;

    public void ReturnToPool()
    {
        PoolManager.Instance.ReturnToPool(this);
    }
}
