using UnityEngine;
using UnityEngine.UI;

public class CharacterFlagView : MonoBehaviour
{
    [SerializeField] private Image _flagImage;

    public void RenderFlag(Sprite flagImage)
    {
        _flagImage.sprite = flagImage;
    }
}
