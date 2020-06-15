using UnityEngine;

public class Destructible : MonoBehaviour
{
    public int ScorePoints;

    PoolItem poolItem;
    void Start()
    {
        poolItem = GetComponent<PoolItem>();
    }

    public void Destroy()
    {
        GameManager.Instance.OnDestructibleDestroyed(this);
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
