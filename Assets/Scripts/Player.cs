using UnityEngine;

public class Player : Character
{
    [SerializeField] private GameController _gameManager;
    [SerializeField] private Sprite _playerIcon;
    [SerializeField] private GameObject _playerArrowContainer;
    [SerializeField] private GameObject _playerArrowViewPrefab;
    [SerializeField] private AnswerInput _answerInput;
    [SerializeField] private QuestionHandler _questionHandler;

    private GameObject _arrowView;
    private int _money;

    public int ShowMoney()
    {
        return _money;
    }

    private void Start()
    {
        _answer = 0;
        ArrowInstantiate();
    }

    private void OnEnable()
    {
        if (PlayerSettingsController.States.Name == null)
            _name = "Player";
        else
            _name = PlayerSettingsController.States.Name;

        if (PlayerSettingsController.States.Flag == null)
            _flag = _playerIcon;
        else
            _flag = PlayerSettingsController.States.Flag;

        _gameManager.PlayerSurvived += OnPlayerSurvived;
        _answerInput.PlayerInputAnswer += OnPlayerInputAnswer;
        _questionHandler.PlayerDropped += OnPlayerDropped;
    }

    private void OnDisable()
    {
        _gameManager.PlayerSurvived -= OnPlayerSurvived;
        _answerInput.PlayerInputAnswer -= OnPlayerInputAnswer;
        _questionHandler.PlayerDropped -= OnPlayerDropped;
    }

    private void OnPlayerInputAnswer(int value)
    {
        _answer = value;
    }

    private void OnPlayerSurvived(int reward)
    {
        _money += reward;
        _arrowView.SetActive(false);
    }

    private void ArrowInstantiate()
    {
        Transform characterPosition = _container.gameObject.transform.GetChild(_container.gameObject.transform.childCount - 1).transform;
        _arrowView = Instantiate(_playerArrowViewPrefab, new Vector3(characterPosition.position.x, characterPosition.position.y, characterPosition.position.z), Quaternion.identity,
            _playerArrowContainer.transform);
    }

    private void OnPlayerDropped()
    {
        _arrowView.SetActive(false);
    }
}
