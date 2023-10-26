using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SurvivedScreen : EndGameScreen
{
    [SerializeField] private Player _player;
    [SerializeField] private TextMeshProUGUI _playerMoneyText;
    [SerializeField] private Button _continueButton;

    public void ShowPlayerMoney()
    {
        _playerMoneyText.text = _player.ShowMoney().ToString();
    }

    private void OnEnable()
    {
        _continueButton.onClick.AddListener(OnContinueButtonClick);
    }

    private void OnDisable()
    {
        _continueButton.onClick.RemoveListener(OnContinueButtonClick);
    }

    private void OnContinueButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1.0f;
    }
}
