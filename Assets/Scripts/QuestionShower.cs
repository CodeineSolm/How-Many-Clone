using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class QuestionShower : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _questionField;

    private List<Question> _questions = new List<Question>();
    private bool _isReady;
    
    private void Start()
    {
        //_questionField = gameObject.GetComponent<TextMeshProUGUI>();
        TakeQuestions();
        _isReady = true;
    }

    private void Update()
    {
        //������ ������ ���� �������, ��� ����� ����� � ������� � �� ������� ��������� ������ (�.�. ������ ���������� ��, � ������ ������ ����� ������ �� ������ � �.�.)
        if (_isReady)
        {
            ShowNext();
            _isReady = false;
        }
    }

    private void TakeQuestions()
    {
        //��� ������ ������ ������� ��������� ��������� �������� � ����� ��� ���� ������?
        _questions.Add(new Question("How many signs are there in Zodiac?", 12));
    }

    private void ShowNext()
    {
        int nextNumber = Random.Range(0, _questions.Count - 1);
        _questionField.text = _questions[nextNumber].GetText();
        _questions.RemoveAt(nextNumber);
    }
}
