using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private QuestionHandler _questionHandler;

    private float _randomAnswerRange = 1.5f; //враги отвечают +-50% от правильного ответа
    private List<string> _names = new List<string>();

    private void OnEnable()
    {
        SetName();
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

    private void SetName()
    {
        //Именя брать из файла?
        _names.Add("John");
        _names.Add("Marsi");
        _names.Add("Harry");
        _names.Add("George");
        _names.Add("Charles");
        _names.Add("Lewis");
        _names.Add("Max");
        _names.Add("Anna");
        _names.Add("Carlos");
        _names.Add("Lando");
        _name = _names[Random.Range(0, _names.Count)];
    }
}
