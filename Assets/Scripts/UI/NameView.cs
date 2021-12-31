using UnityEngine;
using TMPro;

public class NameView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;

    public void Show(string name)
    {
        _nameText.text = name;
    }
}
