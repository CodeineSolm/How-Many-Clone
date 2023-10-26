using System.Collections;
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
    [SerializeField] private QuestionWriter _questionWriter;
    [SerializeField] private Names _names;

    public static string PlayerName;
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
        StartCoroutine(ShowFailedScreen(_showFailedScreenDelay));
    }

    private void OnPlayerSurvived()
    {
        StartCoroutine(ShowSurvivedScreen(_showSurvivedScreenDelay));
        _questionWriter.gameObject.SetActive(false);
        _names.gameObject.SetActive(false);
    }

    private void FreezeTime()
    {
        Time.timeScale = 0;
    }

    private void SpawnSurvivedEffect()
    {
        var spwanObj = Instantiate(_confettiPrefab, new Vector3(_canvas.transform.position.x, _canvas.transform.position.y, _canvas.transform.position.z - 5f), Quaternion.identity);
    }

    private IEnumerator ShowFailedScreen(float delay)
    {
        yield return new WaitForSeconds(delay);
        _failedScreen.Show();
        yield return new WaitForSeconds(_freezeTimeDelay);
        Time.timeScale = 0;
    }

    private IEnumerator ShowSurvivedScreen(float delay)
    {
        yield return new WaitForSeconds(delay);
        PlayerSurvived.Invoke(_surviveReward);
        SpawnSurvivedEffect();
        _survivedScreen.Show();
        _survivedScreen.ShowPlayerMoney();
    }
}
