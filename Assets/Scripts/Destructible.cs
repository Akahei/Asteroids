using UnityEngine;

public class Destructible : MonoBehaviour, IResetable
{
    public GameObject DestroyFXPrefab;

    PoolItem poolItem;
    protected virtual void Awake()
    {
        poolItem = GetComponent<PoolItem>();
    }

    public void Explode(bool SpawnFX = true)
    {
        if (SpawnFX && DestroyFXPrefab != null)
        {
            Instantiate(DestroyFXPrefab, transform.position, transform.rotation);
        } 
        GameManager.Instance.OnDestructibleExplode(this);
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
        if (gameObject.activeInHierarchy) Explode(false); 
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!gameObject.activeInHierarchy) return;
        var otherDestr = other.GetComponentInParent<Destructible>();
        if (otherDestr != null)
        {
            otherDestr.Explode();
            Explode();
        } 
    }
}
