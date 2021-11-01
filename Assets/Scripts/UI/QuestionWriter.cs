using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class QuestionWriter : MonoBehaviour
{
    [SerializeField] private float _timePerCharacter;

    private TextMeshProUGUI _questionField;
    private List<Question> _questions = new List<Question>();
    private float _timer;
    private int _characterIndex;
    private string _questionText;
    private bool _isAllTextWriten;
    private string _answerText;

    private void Start()
    {
        _questionField = GetComponent<TextMeshProUGUI>();
        _characterIndex = 0;
        TakeQuestions();
        _isAllTextWriten = false;
    }

    private void Update()
    {
        WriteQuestion();
    }

    private void TakeQuestions()
    {
        //Тут поидее должно браться несколько рандомных вопросов с файла или базы данных?
        _questions.Add(new Question("How many signs are there in Zodiac?", 12));
        _questions.Add(new Question("How many players are on a basketball team?", 5));
        _questions.Add(new Question("How many stars are on the European Union flag?", 12));
        _questions.Add(new Question("How many 'varieties' are there in Heinz Tomato Ketchup?", 57));
        _questions.Add(new Question("How many deadly sins are there?", 7));
        _questions.Add(new Question("How many eyes do caterpillars have?", 12));
        _questions.Add(new Question("How many varieties of avocados are there?", 500));
        _questions.Add(new Question("How many children did pharaoh Ramses II father?", 160));
        _questions.Add(new Question("How many degrees does the Earth rotate each hour?", 12));
        _questions.Add(new Question("How many players make up a field hockey team?", 11));
    }

    private void GetNext()
    {
        if (_questions.Count != 0)
        {
            int nextNumber = Random.Range(0, _questions.Count - 1);
            _questionText = _questions[nextNumber].GetText();
            _answerText = _questions[nextNumber].GetAnswer().ToString();
            _questions.RemoveAt(nextNumber);
        }
    }

    private void WriteQuestion()
    {
        GetNext();

        if (_isAllTextWriten == false)
        {
            _timer -= Time.deltaTime;

            while (_timer <= 0)
            {
                _timer += _timePerCharacter;
                _characterIndex++;
                string text = _questionText.Substring(0, _characterIndex);
                text += "<color=#00000000>" + _questionText.Substring(_characterIndex) + "</color>";
                _questionField.text = text;

                if (_characterIndex >= _questionText.Length)
                {
                    _isAllTextWriten = true;
                    return;
                }
            }
        }
    }
}
