using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private GameObject endGameScreen;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI title;
    private Image overlayImage;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        overlayImage = endGameScreen?.GetComponent<Image>();
        endGameScreen?.SetActive(false);
    }

    // Оновлення відображення статусів
    public void UpdateScore(int score)
    {
        if (scoreText != null) scoreText.text = score.ToString() + "/10";
    }

    public void UpdateHealth(int health)
    {
        if (healthText != null) healthText.text = health.ToString();
    }

    // Відображення вікна відповідно до виграшу/програшу
    public void ShowEndGameScreen(bool isWin)
    {
        endGameScreen?.SetActive(true);
        if (isWin)
        {
            title.text = "CONGRATULATIONS! YOU WON!";
            overlayImage.color = new Color(0.985f, 0.934f, 0.581f, 0.5f);
        }
        else
        {
            title.text = "GAME OVER";
            overlayImage.color = new Color(0.158f, 0.11f, 0.2f, 0.9f);
        }
    }

    // Методи для кнопок
    public void RestartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}