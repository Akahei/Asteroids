using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public GameObject MenuRoot;
    public GameObject ContinueButton;
    public Text ControlsText;
    public GameObject GameOverPanel;

    void Start()
    {
        SetControlType(InputHandler.Instance.InputType);
        SetMenuActive(true);
        GameOverPanel.SetActive(false);
        GameManager.Instance.OnGameOver += OnGameOver;
    }

    void OnGameOver()
    {
        SetMenuActive(true);
        GameOverPanel.SetActive(true);
    }

    private void Update()
    {
        if(Input.GetButtonDown("Menu") && GameManager.Instance.GameStarted)
        {
            SetMenuActive(!MenuRoot.activeSelf);
        }
    }

    public void Continue()
    {
        SetMenuActive(false);
    }

    public void NewGame()
    {
        GameManager.Instance.NewGame();
        SetMenuActive(false);
        GameOverPanel.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void SetMenuActive(bool active)
    {
        MenuRoot.SetActive(active);
        Time.timeScale = active ? 0f : 1f;
        InputHandler.Instance.ProcessInput = !active;
        ContinueButton.SetActive(GameManager.Instance.GameStarted);
        bool showCursor = active || (InputHandler.Instance.InputType == PlayerInputType.MouseAndKeyboard);
        Cursor.visible = showCursor;
        Cursor.lockState = showCursor ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void SwitchControlType()
    {
        if (InputHandler.Instance.InputType == PlayerInputType.Keyboard)
        {
            SetControlType(PlayerInputType.MouseAndKeyboard);
        }
        else
        {
            SetControlType(PlayerInputType.Keyboard);
        }
    }

    public void SetControlType(PlayerInputType type)
    {
        InputHandler.Instance.SetInputType(type);
        ControlsText.text = "Controls:\n";
        if (InputHandler.Instance.InputType == PlayerInputType.Keyboard)
        {
            ControlsText.text += " keyboard";
        }
        else
        {
            ControlsText.text += " keyboard & mouse";
        }
    }


}
