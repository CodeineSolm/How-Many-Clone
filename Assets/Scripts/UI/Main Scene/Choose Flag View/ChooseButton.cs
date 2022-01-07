using UnityEngine;

public class ChooseButton : MonoBehaviour
{
    [SerializeField] private GameObject _chooseFlagView;

    public void OpenChooseFlagView()
    {
        _chooseFlagView.SetActive(true);
    }
}
