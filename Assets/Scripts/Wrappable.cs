using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrappable : MonoBehaviour
{
    Wrapper wrapper;

    void Start()
    {
        wrapper = GameObject.FindObjectOfType<Wrapper>();
        // DebugUtility.HandleErrorIfNullFindObject<Wrapper, Wrappable>(wrapper, this);

        wrapper.Wrappables.Add(this);
    }

    void Update()
    {

    }
}
