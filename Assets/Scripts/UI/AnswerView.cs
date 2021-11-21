using TMPro;
using UnityEngine;

public class AnswerView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _answerText;

    public void Show(int answer)
    {
        _answerText.text = answer.ToString();
    }
}
