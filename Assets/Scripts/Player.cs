using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private QuestionHandler _questionHandler;
    [SerializeField] private GameController _gameManager;
    [SerializeField] private Sprite _playerIcon;
    [SerializeField] private GameObject _playerArrowContainer;
    [SerializeField] private GameObject _playerArrowViewPrefab;

    public static string Name;
    public static Image Flag;

    private int _money;
    private int _currentHealth;

    public int ShowMoney()
    {
        return _money;
    }

    private void Start()
    {
        _answer = 0;
        ShowPlayerArrow();

        if (_currentHealth <= 0 || _currentHealth >= _maxHealth)
            _currentHealth = _maxHealth;
    }

    private void OnEnable()
    {
        _gameManager.PlayerSurvived += OnPlayerSurvived;
        _questionHandler.PlayerDropped += OnPlayerDropped;

        if (Name == null)
            _name = "Player";
        else
            _name = Name;

        if (Flag == null)
            _flag = _playerIcon;
        else
            _flag = Flag.sprite;
    }

    private void OnDisable()
    {
        _gameManager.PlayerSurvived -= OnPlayerSurvived;
        _questionHandler.PlayerDropped -= OnPlayerDropped;
    }

    private void OnPlayerSurvived(int reward)
    {
        _money += reward;
    }

    private void OnPlayerDropped()
    {
        _currentHealth--;
    }

    private void ShowPlayerArrow()
    {
        Transform characterPosition = _container.gameObject.transform.GetChild(_container.gameObject.transform.childCount - 1).transform;
        var arrowView = Instantiate(_playerArrowViewPrefab, new Vector3(characterPosition.position.x, characterPosition.position.y, characterPosition.position.z), Quaternion.identity,
            _playerArrowContainer.transform);
    }
}
