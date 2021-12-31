using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] TMP_InputField _playerName;

    public void SetPlayerName()
    {
        if (_playerName.text == "")
            Player.PlayerName = "Player";
        else
            Player.PlayerName = _playerName.text;
    }
}
