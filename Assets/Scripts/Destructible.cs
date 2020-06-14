using UnityEngine;
using UnityEngine.Events;

public class Destructible : MonoBehaviour
{
    public UnityAction OnDestory;

    PoolItem poolItem;
    void Start()
    {
        poolItem = GetComponent<PoolItem>();
    }

    public void Destroy()
    {
        if (OnDestory != null) OnDestory.Invoke();
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
