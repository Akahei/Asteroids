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
        LevelBox.Instance.Actors.Add(this);
    }

    protected void OnDestroy()
    {
        LevelBox.Instance.Actors.Remove(this);
    }
}
