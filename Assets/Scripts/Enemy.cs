using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character
{
    [SerializeField] private QuestionHandler _questionHandler;
    [SerializeField] private FlagList _flagList;
    [SerializeField] private NamesList _namesList;

    private float _randomAnswerRange = 1.5f; //враги отвечают +-50% от правильного ответа

    private void OnEnable()
    {
        SetName();
        SetFlag();
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
        _name = _namesList.GetName();
    }
    
    private void SetFlag()
    {
        _flag = _flagList.GetFlag();
    }
}
