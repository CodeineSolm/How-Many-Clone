using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System.Collections;
using UnityEngine.Events;

public class QuestionHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _questionTextField;
    [SerializeField] private AnswerReader _answerReader;
    [SerializeField] private QuestionWriter _questionWriter;
    [SerializeField] private List<Character> _characters = new List<Character>();

    public event UnityAction Answered;

    private int _correctAnswer;

    private void Start()
    {
        _correctAnswer = _questionWriter.GetCorrectAnswer();
    }

    private void Update()
    {

    }

    private void OnEnable()
    {
        _answerReader.SubmitButtonClicked += OnSubmitButtonClicked;
        _questionWriter.Written += OnQuestionWritten;
    }

    private void OnDisable()
    {
        _answerReader.SubmitButtonClicked -= OnSubmitButtonClicked;
        _questionWriter.Written -= OnQuestionWritten;
    }

    private void OnQuestionWritten()
    {
        _correctAnswer = _questionWriter.GetCorrectAnswer();
    }

    private void OnSubmitButtonClicked(int playerAnswer)
    {
        _questionTextField.text = _correctAnswer.ToString();
        StartCoroutine(CompareAnswers());
    }

    private IEnumerator CompareAnswers()
    {
        yield return new WaitForSeconds(0.5f);
        List<Character> charactersWithCorrectAnswers = new List<Character>();
        List<Character> charactersWithClosestAnswers = new List<Character>();
        List<Character> charactersWithSecondAnswers = new List<Character>();
        List<Character> charactersWithThirdAnswers = new List<Character>();
        List<Character> charactersWithFourthAnswers = new List<Character>();
        List<Character> charactersTempOne = new List<Character>();
        List<Character> charactersTempTwo = new List<Character>();
        _characters = _characters.OrderBy(x => System.Math.Abs(x.GetAnswer() - _correctAnswer)).ToList();

        foreach (var character in _characters)
        {
            character.ShowAnswer();

            if (character.GetAnswer() == _correctAnswer)
                charactersWithCorrectAnswers.Add(character);
            else
                charactersTempOne.Add(character);
        }

        int nextAnswer = charactersTempOne[0].GetAnswer();

        foreach (var character in charactersTempOne)
        {
            if (character.GetAnswer() == nextAnswer)
            {
                if (charactersWithCorrectAnswers.Count == 0)
                    charactersWithClosestAnswers.Add(character);
                else
                    charactersWithSecondAnswers.Add(character);
            }
            else
                charactersTempTwo.Add(character);
        }

        charactersTempOne = new List<Character>();

        if (charactersTempTwo.Count != 0)
        {
            nextAnswer = charactersTempTwo[0].GetAnswer();

            foreach (var character in charactersTempTwo)
            {
                if (character.GetAnswer() == nextAnswer)
                {
                    if (charactersWithCorrectAnswers.Count == 0)
                        charactersWithSecondAnswers.Add(character);
                    else
                        charactersWithThirdAnswers.Add(character);
                }
                else
                    charactersTempOne.Add(character);
            }
        }
        
        charactersTempTwo = new List<Character>();

        if (charactersTempOne.Count != 0)
        {
            nextAnswer = charactersTempOne[0].GetAnswer();

            foreach (var character in charactersTempOne)
            {
                if (character.GetAnswer() == nextAnswer)
                {
                    if (charactersWithCorrectAnswers.Count == 0)
                        charactersWithThirdAnswers.Add(character);
                    else
                        charactersWithFourthAnswers.Add(character);
                }
                else
                    charactersTempTwo.Add(character);
            }
        }
        
        if (charactersTempTwo.Count != 0)
        {
            nextAnswer = charactersTempTwo[0].GetAnswer();

            foreach (var character in charactersTempOne)
            {
                if (character.GetAnswer() == nextAnswer)
                    charactersWithFourthAnswers.Add(character);
            }
        }

        bool isSecondIsLastAnswer = false;
        bool isThirdIsLastAnswer = false;
        bool isFourthIsLastAnswer = false;

        if (charactersWithFourthAnswers.Count != 0)
        {
            isFourthIsLastAnswer = true;
        }
        else if (charactersWithThirdAnswers.Count != 0)
        {
            isThirdIsLastAnswer = true;
        }
        else if (charactersWithSecondAnswers.Count != 0)
        {
            isSecondIsLastAnswer = true;
        }

        yield return new WaitForSeconds(0.5f);
        ShowAnswerResults(charactersWithCorrectAnswers, " correct!");
        ShowAnswerResults(charactersWithClosestAnswers, " closest!");
        ShowAnswerResults(charactersWithSecondAnswers, " second!", isSecondIsLastAnswer);
        ShowAnswerResults(charactersWithThirdAnswers, " third!", isThirdIsLastAnswer);
        ShowAnswerResults(charactersWithFourthAnswers, " fourth!", isFourthIsLastAnswer);
        yield return new WaitForSeconds(0.5f);
        Answered?.Invoke();
    }

    private void ShowAnswerResults(List<Character> characters, string resultString, bool isFourth = false)
    {
        if (characters.Count > 0)
        {
            foreach (var character in characters)
            {
                Debug.Log(character.transform.name.ToString() + " " + resultString);
                if (isFourth)
                    character.Drop();
            }
        }
    }
}
