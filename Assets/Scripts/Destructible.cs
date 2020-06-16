using UnityEngine;

public class Destructible : MonoBehaviour, IResetable
{
    PoolItem poolItem;
    protected virtual void Awake()
    {
        poolItem = GetComponent<PoolItem>();
    }

    public void Destroy()
    {
        GameManager.Instance.OnDestructibleDestroyed(this);
        if (poolItem != null)
        {
            poolItem.ReturnToPool();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetObject()
    {
        if (gameObject.activeInHierarchy) Destroy(); 
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!gameObject.activeInHierarchy) return;
        var otherDestr = other.GetComponentInParent<Destructible>();
        if (otherDestr != null)
        {
            otherDestr.Destroy();
            Destroy();
        } 
    }
}
