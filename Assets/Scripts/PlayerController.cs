using UnityEngine;

[RequireComponent(typeof(InputHandler))]
public class PlayerController : MonoBehaviour
{

    Ship playerShip;
    InputHandler inputHandler;
    PlayerInputType inputType => inputHandler.InputType;
    // Start is called before the first frame update

    private void Awake()
    {
        playerShip = GetComponent<Ship>();
        inputHandler = GetComponent<InputHandler>();
    }

    void Start()
    {
    }

    // Update is called once per frame
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
