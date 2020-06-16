using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerInputType
{
    Keyboard,
    MouseAndKeyboard
}

public class InputHandler : MonoBehaviour
{
    public PlayerInputType InputType = PlayerInputType.MouseAndKeyboard;
    public bool ProcessInput = false;

    static public InputHandler Instance;

    void Awake()
    {
        Instance = this;
    }

    public bool IsForwardInputDown()
    {
        if (!ProcessInput) return false;
        return Input.GetButton("Forward") || (IsUsingMouse() && Input.GetButton("Forward_m"));
    }

    public bool IsFireInputDown()
    {
        if (!ProcessInput) return false;
        return Input.GetButtonDown("Fire") || (IsUsingMouse() && Input.GetButtonDown("Fire_m"));
    }

    public float GetRotationInput()
    {
        if (!ProcessInput) return 0f;
        return Input.GetAxis("Rotation");
    }

    public Vector3 GetMouseInWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    bool IsUsingMouse() => InputType == PlayerInputType.MouseAndKeyboard;
}
