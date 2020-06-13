using UnityEngine;
using UnityEngine.Events;

public class Actor : MonoBehaviour
{
    public UnityAction OnDied;

    public void Die()
    {
        if (OnDied != null)
        {
            OnDied.Invoke();
        }
        HandleDie();
    }

    protected virtual void HandleDie()
    {
        Destroy(gameObject);
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
