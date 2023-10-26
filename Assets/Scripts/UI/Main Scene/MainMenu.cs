using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_InputField _playerName;
    [SerializeField] private Image _flagImage;

    public void SetPlayerSettings()
    {
        if (_playerName.text == "")
            PlayerSettingsController.States.Name = "Player";
        else
            PlayerSettingsController.States.Name = _playerName.text;

        PlayerSettingsController.States.Flag = _flagImage.sprite;
    }
}
