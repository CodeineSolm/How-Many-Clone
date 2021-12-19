using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private QuestionHandler _questionHandler;

    private float _randomAnswerRange = 1.5f; //враги отвечают +-50% от правильного ответа

    private void OnEnable()
    {
        _questionHandler.PlayerAnswered += OnPlayerAnswered;
    }

    private void OnDisable()
    {
        _questionHandler.PlayerAnswered -= OnPlayerAnswered;
    }

    private void OnPlayerAnswered(int correctAnswer)
    {
        int minRandomAnswer = (int)(correctAnswer * _randomAnswerRange);
        int maxRandomAnswer = (int)(correctAnswer / _randomAnswerRange);
        _answer = Random.Range(minRandomAnswer, maxRandomAnswer);
    }
}
