using UnityEngine;

public class Player : Character
{
    [SerializeField] private AnswerReader _answerReader;

    private void OnEnable()
    {
        _answerReader.SubmitButtonClicked += OnSubmitButtonClick;
    }

    private void OnDisable()
    {
        _answerReader.SubmitButtonClicked -= OnSubmitButtonClick;
    }

    private void OnSubmitButtonClick(int playerAnswer)
    {
        _answer = playerAnswer;
    }
}
