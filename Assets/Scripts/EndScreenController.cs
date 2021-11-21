using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenController : MonoBehaviour
{
    [SerializeField] private QuestionHandler _questionHandler;

    private void OnEnable()
    {
        _questionHandler.PlayerDropped += OnPlayerDropped;
        _questionHandler.PlayerSurvived += OnPlayerSurvived;
    }

    private void OnDisable()
    {
        _questionHandler.PlayerDropped -= OnPlayerDropped;
        _questionHandler.PlayerSurvived -= OnPlayerSurvived;
    }

    private void OnPlayerDropped()
    {
        Debug.Log("You failed!");
    }

    private void OnPlayerSurvived()
    {
        Debug.Log("You win!");
    }
}
