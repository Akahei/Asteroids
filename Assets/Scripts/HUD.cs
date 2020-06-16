using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public GameObject HUDRoot;
    public Text ScoreText;
    public Text LifesText;

    void Start()
    {
        GameManager.Instance.OnGameStarted += OnGameStarted;
        GameManager.Instance.OnScoreChanged += OnScoreChanged;
        GameManager.Instance.OnLifesChanged += OnLifesChanged;
        HUDRoot.SetActive(false);
    }

    void OnScoreChanged(int value)
    {
        ScoreText.text = value.ToString();
    }

    void OnLifesChanged(int value)
    {
        LifesText.text = value.ToString();
    }

    void OnGameStarted()
    {
        HUDRoot.SetActive(true);
    }
}
