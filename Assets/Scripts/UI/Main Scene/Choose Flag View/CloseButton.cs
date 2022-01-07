using UnityEngine;

public class CloseButton : MonoBehaviour
{
    [SerializeField] private GameObject _chooseFlagView;

    public void CloseFlagView()
    {
        _chooseFlagView.SetActive(false);
    }
}
