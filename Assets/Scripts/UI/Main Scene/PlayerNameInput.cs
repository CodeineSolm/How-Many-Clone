using TMPro;
using UnityEngine;

public class PlayerNameInput : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _playerNameText;
    [SerializeField] private TextMeshProUGUI _playerNamePlaceholder;

    private void Start()
    {
        //_playerNamePlaceholder.text = _playerNameText.text;
    }
}
