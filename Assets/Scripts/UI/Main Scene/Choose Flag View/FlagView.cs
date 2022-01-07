using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FlagView : MonoBehaviour
{
    [SerializeField] private Image _flagImage;
    [SerializeField] private Button _flagViewButton;

    public event UnityAction<Image> FlagViewClicked;

    public void Render(Flag flag)
    {
        _flagImage.sprite = flag.Image;
    }

    public void ClickOnFlagView()
    {
        FlagViewClicked?.Invoke(_flagImage);
    }
}
