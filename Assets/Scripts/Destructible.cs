using UnityEngine;

public class Destructible : MonoBehaviour, IResetable
{
    public GameObject DestroyFXPrefab;

    PoolItem poolItem;
    protected virtual void Awake()
    {
        poolItem = GetComponent<PoolItem>();
    }

    public void Explode(GameObject instigator, bool SpawnFX = true)
    {
        if (SpawnFX && DestroyFXPrefab != null)
        {
            Instantiate(DestroyFXPrefab, transform.position, transform.rotation);
        } 
        GameManager.Instance.OnDestructibleExplode(this, instigator);
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
        if (gameObject.activeInHierarchy) Explode(null, false); 
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!gameObject.activeInHierarchy) return;
        var otherDestr = other.GetComponentInParent<Destructible>();
        if (otherDestr != null)
        {
            otherDestr.Explode(this.gameObject);
            Explode(otherDestr.gameObject);
        } 
    }
}
