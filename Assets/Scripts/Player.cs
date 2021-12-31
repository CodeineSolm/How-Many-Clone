using UnityEngine;

public class Player : Character
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private AnswerReader _answerReader;
    [SerializeField] private QuestionHandler _questionHandler;
    [SerializeField] private GameController _gameManager;

    public static string PlayerName;

    private int _money;
    private int _currentHealth;

    public int ShowMoney()
    {
        return _money;
    }

    private void Start()
    {
        if (_currentHealth <= 0 || _currentHealth >= _maxHealth)
            _currentHealth = _maxHealth;
    }

    private void OnEnable()
    {
        _answerReader.SubmitButtonClicked += OnSubmitButtonClick;
        _gameManager.PlayerSurvived += OnPlayerSurvived;
        _questionHandler.PlayerDropped += OnPlayerDropped;
        _name = PlayerName;
    }

    private void OnDisable()
    {
        _answerReader.SubmitButtonClicked -= OnSubmitButtonClick;
        _gameManager.PlayerSurvived -= OnPlayerSurvived;
        _questionHandler.PlayerDropped -= OnPlayerDropped;
    }

    private void OnSubmitButtonClick(int playerAnswer)
    {
        _answer = playerAnswer;
    }

    private void OnPlayerSurvived(int reward)
    {
        _money += reward;
    }

    private void OnPlayerDropped()
    {
        _currentHealth--;
    }
}
