using UnityEngine;
using UnityEngine.Events;

public class Actor : MonoBehaviour
{
    public UnityAction OnDied;
    private PoolObject poolObject;

    void Awake()
    {
        poolObject = GetComponent<PoolObject>();
    }

    public void Die()
    {
        if (OnDied != null)
        {
            OnDied.Invoke();
        }
        if (poolObject)
        {
            poolObject.ReturnToPool();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected void Start()
    {
        Wrapper.Instance.Actors.Add(this);
    }

    protected void OnDestroy()
    {
        Wrapper.Instance.Actors.Remove(this);
    }
}
