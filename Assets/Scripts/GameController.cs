using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private QuestionHandler _questionHandler;
    [SerializeField] private SurvivedScreen _survivedScreen;
    [SerializeField] private FailedScreen _failedScreen;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private float _freezeTimeDelay = 1.1f;
    [SerializeField] private float _showScreenDelay = 3f;

    private void OnEnable()
    {
        _questionHandler.PlayerDropped += OnPlayerDropped;
        _questionHandler.PlayerSurvived += OnPlayerSurvived;
    }

    private void OnDisable()
    {
        _questionHandler.PlayerDropped -= OnPlayerDropped;
        _questionHandler.PlayerSurvived -= OnPlayerSurvived;
    }

    private void OnPlayerDropped()
    {
        Invoke("ShowFailedScreen", _showScreenDelay);
    }

    private void OnPlayerSurvived()
    {
        Invoke("ShowSurvivedScreen", _showScreenDelay);
    }

    private void ShowSurvivedScreen()
    {
        _survivedScreen.Show();
        Invoke("FreezeTime", _freezeTimeDelay);
    }

    private void ShowFailedScreen()
    {
        _failedScreen.Show();
        Invoke("FreezeTime", _freezeTimeDelay);
    }

    private void FreezeTime()
    {
        Time.timeScale = 0;
    }
}
