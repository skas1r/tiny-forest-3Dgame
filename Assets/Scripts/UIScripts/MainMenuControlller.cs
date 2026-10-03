using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControlller : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#endif
    }
}
