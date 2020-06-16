using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Ship playerShip;
    InputHandler inputHandler => InputHandler.Instance;
    PlayerInputType inputType => inputHandler.InputType;

    private void Awake()
    {
        playerShip = GetComponent<Ship>();
    }

    void Update()
    {
        if (inputType == PlayerInputType.Keyboard)
        {
            playerShip.Rotate(inputHandler.GetRotationInput());
        }
        else 
        {
            playerShip.RotateTowards(inputHandler.GetMouseInWorldPosition());
        }
        if (inputHandler.IsForwardInputDown())
        {
            playerShip.Accelerate();
        }

        if (inputHandler.IsFireInputDown())
        {
            playerShip.Fire();
        }
    }
}
