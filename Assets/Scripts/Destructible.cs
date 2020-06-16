using UnityEngine;

public class Destructible : MonoBehaviour, IResetable
{
    PoolItem poolItem;
    void Awake()
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

    public void ResetObject()
    {
        Destroy();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        var otherDestr = other.GetComponentInParent<Destructible>();
        if (otherDestr != null)
        {
            otherDestr.Destroy();
            Destroy();
        } 
    }
}
