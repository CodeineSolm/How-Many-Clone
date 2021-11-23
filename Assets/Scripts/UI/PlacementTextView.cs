using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class PlacementTextView : MonoBehaviour
{
    private TextMeshProUGUI _placementText;

    private void Awake()
    {
        _placementText = GetComponent<TextMeshProUGUI>();
    }

    public void Show(string placementText)
    {
        _placementText.text = placementText;
    }

    public void Hide()
    {
        _placementText.text = "";
    }
}
