using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_InputField _playerName;
    [SerializeField] private Image _flagImage;

    public void SetPlayerName()
    {
        if (_playerName.text == "")
            Player.Name = "Player";
        else
            Player.Name = _playerName.text;

        Player.Flag = _flagImage;
    }
}
