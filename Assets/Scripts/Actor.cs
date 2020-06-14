using UnityEngine;
using UnityEngine.Events;

public class Actor : MonoBehaviour
{
    public UnityAction OnDied;
    private PoolItem poolItem;

    void Awake()
    {
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
