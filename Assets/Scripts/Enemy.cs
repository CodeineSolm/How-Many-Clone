using UnityEngine;

public class Enemy : Character
{
    private float _randomAnswerRange = 0.8f; //+-80%

    public override int GetAnswer(int correctAnswer)
    {
        int minRandomAnswer = (int)(correctAnswer * _randomAnswerRange);
        int maxRandomAnswer = (int)(correctAnswer / _randomAnswerRange);
        _answer = Random.Range(minRandomAnswer, maxRandomAnswer);
        Debug.Log(transform.name.ToString() + " answer: " + _answer);
        return _answer;
    }
}
