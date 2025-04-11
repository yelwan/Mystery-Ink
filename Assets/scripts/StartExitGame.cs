using UnityEngine;
using UnityEngine.SceneManagement;

public class StartExitGame : MonoBehaviour
{
    public void StartNewGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
