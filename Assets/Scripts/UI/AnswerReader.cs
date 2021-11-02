using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class AnswerReader : MonoBehaviour
{
    [SerializeField] private QuestionWriter _questionWriter;
    [SerializeField] private TMP_InputField _answerInputField;
    [SerializeField] private Button _submitButton;
    [SerializeField] private int _charactersLimit;

    public event UnityAction<int> SubmitButtonClicked;

    private const string _validCharacters = "0123456789";

    public void OnSubmitButtonClick()
    {
        int playerAnswer = Convert.ToInt32(_answerInputField.text);
        SubmitButtonClicked?.Invoke(playerAnswer);
        Hide();
    }

    private void Awake()
    {
        Hide();
    }

    private void OnEnable()
    {
        _questionWriter.Written += OnQuestionWriten;
    }

    private void OnDisable()
    {
        _questionWriter.Written -= OnQuestionWriten;
    }

    private void OnQuestionWriten()
    {
        Show();
    }

    private void Show()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(true);
        }

        _answerInputField.characterLimit = _charactersLimit;
        _answerInputField.onValidateInput = (string text, int charIndex, char addedChar) =>
        {
            return ValidateChar(addedChar);
        };
    }

    private void Hide()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private char ValidateChar(char addedChar)
    {
        if (_validCharacters.IndexOf(addedChar) != -1)
            return addedChar;
        else
            return '\0';
    }
}
