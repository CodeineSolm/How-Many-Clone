using UnityEngine;
using UnityEngine.SceneManagement;

public class FailedScreen : EndGameScreen
{
    public void OnRestartButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1.0f;
    }
}
