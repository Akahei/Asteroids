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

    public bool IsForwardInputDown()
    {
        return Input.GetButton("Forward");
    }

    public bool IsFireInputDown()
    {
        return Input.GetButtonDown("Fire");
    }

    public float GetRotationInput()
    {
        return Input.GetAxis("Rotation");
    }

    public Vector3 GetMouseInWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
