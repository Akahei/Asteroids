using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PoolManager : MonoBehaviour
{
    static public PoolManager Instance { get; private set; }

    Dictionary<GameObject, Queue<GameObject>> Pools = new Dictionary<GameObject, Queue<GameObject>>();

    void Awake()
    {
        Instance = this;
    }

    public GameObject SpawnObject(GameObject prefab, Vector3 pos, Quaternion rot)
    {
        if (!Pools.ContainsKey(prefab))
        {
            Pools.Add(prefab, new Queue<GameObject>());
        }

        var pool = Pools[prefab];
        if (pool.Count > 0)
        {
            var obj = pool.Dequeue();
            obj.transform.position = pos;
            obj.transform.rotation = rot;
            obj.SetActive(true);
            return obj;
        }

        var newInstance = Instantiate(prefab, pos, rot);;
        var instanceActor = newInstance.GetComponent<PoolActor>();
        Assert.IsNotNull(instanceActor);
        instanceActor.OriginPrefab = prefab;
        return newInstance;
    }

    public void ReturnToPool(GameObject obj)
    {
        var actor = obj.GetComponent<PoolActor>();
        Pools[actor.OriginPrefab].Enqueue(obj);
        obj.SetActive(false);
    }

}
