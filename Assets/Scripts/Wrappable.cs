using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrappable : MonoBehaviour
{
    Wrapper wrapper;

    void Start()
    {
        wrapper = GameObject.FindObjectOfType<Wrapper>();
        wrapper.Wrappables.Add(this);
    }

    void OnDestroy()
    {
        wrapper.Wrappables.Remove(this);
    }
}
