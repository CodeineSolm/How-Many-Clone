using GestureRecognizer;
using UnityEngine;

public class AnswerInput : MonoBehaviour
{
    [SerializeField] private QuestionWriter _questionWriter;
    [SerializeField] private DrawDetector _drawArea;
    [SerializeField] private QuestionHandler _questionHandler;

    private void Start()
    {
        _drawArea.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _questionWriter.Written += OnWritten;
        _questionHandler.Answered += OnAnswered;
    }

    private void OnDisable()
    {
        _questionWriter.Written -= OnWritten;
        _questionHandler.Answered -= OnAnswered;
    }

    private void OnWritten()
    {
        _drawArea.gameObject.SetActive(true);
    }

    private void OnAnswered()
    {
        _drawArea.ClearLines();
    }
}
