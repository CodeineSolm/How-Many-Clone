
using UnityEngine;

public class Player : Character
{
    [SerializeField] private QuestionHandler _questionHandler;

    private void OnEnable()
    {
        _questionHandler.IncorrectAnswer += OnIncorrectAnswer;
    }

    private void OnDisable()
    {
        _questionHandler.IncorrectAnswer -= OnIncorrectAnswer;
    }

    private void OnIncorrectAnswer()
    {
        _container.transform.GetChild(0).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }
}
