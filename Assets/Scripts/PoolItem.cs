﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolItem : MonoBehaviour
{
    [HideInInspector] 
    public Object OriginPrefab;
    public Object InstancedObject;

    public void ReturnToPool()
    {
        PoolManager.PMInstance.ReturnToPool(this);
    }
}
