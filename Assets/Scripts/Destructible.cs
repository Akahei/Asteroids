using UnityEngine;
using UnityEngine.Events;

public class Destructible : MonoBehaviour
{
    public UnityAction OnDestory;
    public int ScorePoints;

    PoolItem poolItem;
    void Start()
    {
        poolItem = GetComponent<PoolItem>();
    }

    public void Destroy()
    {
        GameManager.Instance.OnDestructibleDestroyed(this);
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
