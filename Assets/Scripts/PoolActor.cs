using UnityEngine;

public class PoolActor : Actor
{
    [HideInInspector] 
    public GameObject OriginPrefab;

    protected override void HandleDie()
    {
        PoolManager.Instance.ReturnToPool(gameObject);
    }
}