using TMPro;
using UnityEngine;

public class SurvivedScreen : EndGameScreen
{
    [SerializeField] private float _showMoneyDelay;
    [SerializeField] private Player _player;
    [SerializeField] private TextMeshProUGUI _playerMoneyText;

    public void ShowPlayerMoney()
    {
        _playerMoneyText.text = _player.ShowMoney().ToString();
    }
}
