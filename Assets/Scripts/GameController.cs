using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    [SerializeField] private int _surviveReward;
    [SerializeField] private QuestionHandler _questionHandler;
    [SerializeField] private SurvivedScreen _survivedScreen;
    [SerializeField] private FailedScreen _failedScreen;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private GameObject _confettiPrefab;
    [SerializeField] private float _freezeTimeDelay;
    [SerializeField] private float _showSurvivedScreenDelay;
    [SerializeField] private float _showFailedScreenDelay;

    public event UnityAction<int> PlayerSurvived;

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
        Invoke("ShowFailedScreen", _showFailedScreenDelay);
    }

    private void OnPlayerSurvived()
    {
        Invoke("ShowSurvivedScreen", _showSurvivedScreenDelay);
    }

    private void ShowSurvivedScreen()
    {
        PlayerSurvived.Invoke(_surviveReward);
        SpawnSurvivedEffect();
        _survivedScreen.Show();
        _survivedScreen.ShowPlayerMoney();
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

    private void SpawnSurvivedEffect()
    {
        var spwanObj = Instantiate(_confettiPrefab, new Vector3(_canvas.transform.position.x, _canvas.transform.position.y, _canvas.transform.position.z - 5f), Quaternion.identity);
    }
}
