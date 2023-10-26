using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class AnswerInput : MonoBehaviour
{
    [SerializeField] private QuestionWriter _questionWriter;
    [SerializeField] private TMP_Text _answerText;
    [SerializeField] private GameObject _panel;

    public event UnityAction<int> PlayerInputAnswer;

    public void OnNumberButtonClick(int value)
    {
        _answerText.text += value.ToString();
    }

    public void OnClearButtonClick()
    {
        _answerText.text = string.Empty;
    }

    public void OnEnterButtonClick()
    {
        int playerAnswer = int.Parse(_answerText.text);
        PlayerInputAnswer?.Invoke(playerAnswer);
        _answerText.text = string.Empty;
        _panel.SetActive(false);
    }

    private void Start()
    {
        _panel.SetActive(false);
    }

    private void OnEnable()
    {
        _questionWriter.Written += OnWritten;
    }

    private void OnDisable()
    {
        _questionWriter.Written -= OnWritten;
    }

    private void OnWritten()
    {
        _panel.SetActive(true);
    }
}
