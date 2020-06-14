using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Object = UnityEngine.Object;

public class PoolManager : MonoBehaviour
{
    static public PoolManager PMInstance { get; private set; }

    Dictionary<Object, Queue<Object>> Pools = new Dictionary<Object, Queue<Object>>();

    void Awake()
    {
        PMInstance = this;
    }

    public T GetInstance<T>(T prefab) where T : Object
    {
        if (!Pools.ContainsKey(prefab))
        {
            Pools.Add(prefab, new Queue<Object>());
        }

        var pool = Pools[prefab];
        if (pool.Count > 0)
        {
            var instance = pool.Dequeue();
            GetGameObject(instance).SetActive(true);
            return instance as T;
        }

        var newInstance = Instantiate(prefab);
        var poolItem = GetGameObject(newInstance).GetComponent<PoolItem>();
        Assert.IsNotNull(poolItem);
        poolItem.OriginPrefab = prefab;
        poolItem.InstancedObject = newInstance;
        return newInstance;
    }

    public void ReturnToPool(PoolItem item)
    {
        Pools[item.OriginPrefab].Enqueue(item.InstancedObject);
        GetGameObject(item.InstancedObject).SetActive(false);
    }

    void SetActive(Object obj, bool active)
    {
        GetGameObject(obj).SetActive(active);
    }

    GameObject GetGameObject(Object obj)
    {
        if (obj is Component component)
        {
            return component.gameObject;
        }
        return obj as GameObject;
    }
}
