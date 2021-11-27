using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private QuestionHandler _questionHandler;
    [SerializeField] private SurvivedScreen _survivedScreen;
    [SerializeField] private FailedScreen _failedScreen;
    [SerializeField] private GameObject _canvas;

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
        Invoke("ShowFailedScreen", 3f);
    }

    private void OnPlayerSurvived()
    {
        Invoke("ShowSurvivedScreen", 3f);
    }

    private void ShowSurvivedScreen()
    {
        InstantiateScreen(_survivedScreen);
        Invoke("FreezeTime", 0.5f);
    }

    private void ShowFailedScreen()
    {
        InstantiateScreen(_failedScreen);
        Invoke("FreezeTime", 0.5f);
    }

    private void FreezeTime()
    {
        Time.timeScale = 0;
    }

    private void InstantiateScreen(EndGameScreen endGameScreen)
    {
        Instantiate(endGameScreen, new Vector3(_canvas.transform.position.x, _canvas.transform.position.y, _canvas.transform.position.z),
            Quaternion.identity, _canvas.transform);
    }
}
