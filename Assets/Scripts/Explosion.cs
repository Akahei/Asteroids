using UnityEngine;

[RequireComponent(typeof(PoolItem))]
public class Explosion : MonoBehaviour
{
    PoolItem poolItem;
    AudioSource audioSource;

    void Awake()
    {
        poolItem = GetComponent<PoolItem>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            poolItem.ReturnToPool();
        }
    }
}
