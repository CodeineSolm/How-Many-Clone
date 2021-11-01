using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Question
{
    private string _questionText;
    private int _answer;

    public Question(string questionText, int answer)
    {
        _questionText = questionText;
        _answer = answer;
    }

    public string GetText()
    {
        return _questionText;
    }

    public int GetAnswer()
    {
        return _answer;
    }
}
