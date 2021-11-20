using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private AnswerReader _answerReader;

    private float _randomAnswerRange = 0.8f; //+-80%

    private void OnEnable()
    {
        _answerReader.SubmitButtonClicked += OnSubmitButtonClicked;
    }

    private void OnDisable()
    {
        _answerReader.SubmitButtonClicked -= OnSubmitButtonClicked;
    }

    private void OnSubmitButtonClicked(int playerAnswer)
    {
        int minRandomAnswer = (int)(playerAnswer * _randomAnswerRange);
        int maxRandomAnswer = (int)(playerAnswer / _randomAnswerRange);
        _answer = Random.Range(minRandomAnswer, maxRandomAnswer);
    }
}
