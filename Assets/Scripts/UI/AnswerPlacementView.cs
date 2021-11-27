using UnityEngine;

public class AnswerPlacementView : MonoBehaviour
{
    [SerializeField] private PlacementTextView _placementTextViewPrefab;

    private float _placementTextShiftY = 0.5f;

    public void Show(string placementText, Transform characterPosition, Color textColor)
    {
        var placementTextView = Instantiate(_placementTextViewPrefab, new Vector3(characterPosition.position.x, characterPosition.position.y - _placementTextShiftY, 
            characterPosition.position.z), Quaternion.identity, gameObject.transform);
        placementTextView.Show(placementText, textColor);
    }

    public void Hide()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.GetComponent<PlacementTextView>().Hide();
        }
    }
}
