using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System.Collections;
using UnityEngine.Events;

public class QuestionHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _questionTextField;
    [SerializeField] private QuestionWriter _questionWriter;
    [SerializeField] private Answer _answers;
    [SerializeField] private AnswerPlacementView _answersPlacementsView;
    [SerializeField] private List<Character> _characters = new List<Character>();
    [SerializeField] private Water _water;
    [SerializeField] private MouseEvent _mouseEvent;

    public event UnityAction Answered;
    public event UnityAction PlayerDropped;
    public event UnityAction<int> PlayerAnswered;
    public event UnityAction PlayerSurvived;

    private int _correctAnswer;
    private bool _isPlayerDropped = false;
    private Color _correctAnswerColor = Color.green;
    private Color _closestAnswerColor = Color.green;
    private Color32 _secondAnswerColor = Color.yellow;
    private Color32 _thirdAnswerColor = new Color32(255, 138, 0, 255);
    private Color _fourthAnswerColor = Color.red;
    private const string _correctAnswerText = "Correct";
    private const string _closestAnswerText = "Closest";
    private const string _secondAnswerText = "Second";
    private const string _thirdAnswerText = "Third";
    private const string _fourthAnswerText = "Fourth";
    private float _secondAnswerFallDistance = 1f;
    private float _thirdAnswerFallDistance = 1.5f;
    private float _fourthAnswerFallDistance = 2f;

    private void Start()
    {
        _correctAnswer = _questionWriter.GetCorrectAnswer();

        foreach (var character in _characters)
            character.ShowInfo();
    }

    private void OnEnable()
    {
        _questionWriter.Written += OnQuestionWritten;
        _water.CharacterDropped += OnCharacterDropped;
        _mouseEvent.PointerUp += OnPointerUp;
    }

    private void OnDisable()
    {
        _questionWriter.Written -= OnQuestionWritten;
        _water.CharacterDropped -= OnCharacterDropped;
        _mouseEvent.PointerUp -= OnPointerUp;
    }

    private void OnPointerUp()
    {
        _questionTextField.text = _correctAnswer.ToString();
        PlayerAnswered?.Invoke(_correctAnswer);
        StartCoroutine(CompareAnswers());
    }

    private void OnQuestionWritten()
    {
        _correctAnswer = _questionWriter.GetCorrectAnswer();
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

        _answers.gameObject.SetActive(true);

        if (_characters.Count != charactersWithCorrectAnswers.Count)
        {
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
        }

        yield return new WaitForSeconds(1f);
        _answers.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        ShowAnswerResults(charactersWithCorrectAnswers, _correctAnswerText, _correctAnswerColor);
        ShowAnswerResults(charactersWithClosestAnswers, _closestAnswerText, _closestAnswerColor);
        ShowAnswerResults(charactersWithSecondAnswers, _secondAnswerText, _secondAnswerColor, isSecondAnswer: true);
        ShowAnswerResults(charactersWithThirdAnswers, _thirdAnswerText, _thirdAnswerColor, isThirdAnswer: true);
        ShowAnswerResults(charactersWithFourthAnswers, _fourthAnswerText, _fourthAnswerColor, isFourthAnswer: true);
        yield return new WaitForSeconds(1f);
        _answersPlacementsView.Hide();

        if (_characters.Count == 1 && _characters[0].GetComponent<Player>() != null)
            PlayerSurvived?.Invoke();
        else if (_isPlayerDropped == false)
            Answered?.Invoke();
    }

    private void ShowAnswerResults(List<Character> characters, string placementText, Color textColor, bool isSecondAnswer = false, bool isThirdAnswer = false, bool isFourthAnswer = false)
    {
        if (characters.Count > 0)
        {
            foreach (var character in characters)
            {
                Transform characterPosition = character.gameObject.transform.GetChild(character.gameObject.transform.childCount - 1).transform;
                _answersPlacementsView.Show(placementText, characterPosition, textColor);

                if (isSecondAnswer)
                    character.Fall(_secondAnswerFallDistance);
                else if (isThirdAnswer)
                    character.Fall(_thirdAnswerFallDistance);
                else if (isFourthAnswer)
                    character.Fall(_fourthAnswerFallDistance);
            }
        }
    }

    private void OnCharacterDropped(GameObject gameObject)
    {
        gameObject.transform.parent.TryGetComponent<Character>(out Character character);
        character.Drop();
        _characters.Remove(character);

        if (character.gameObject.TryGetComponent<Player>(out Player player))
        {
            PlayerDropped?.Invoke();
            _isPlayerDropped = true;
        }
    }
}
