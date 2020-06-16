using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkAnimation : MonoBehaviour
{
    public float BlinkPeriod = 0.25f;
    Renderer[] renderers;
    float timer;
    bool nextState = false;

    void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > BlinkPeriod)
        {
            timer = 0;
            foreach(var rend in renderers)
            {
                rend.enabled = nextState;
            }
            nextState = !nextState;
        }
    }

    private void OnEnable()
    {
        timer = 0;
        nextState = false;
    }

    private void OnDisable()
    {
        foreach(var rend in renderers)
        {
            rend.enabled = true;
        }
    }
}
