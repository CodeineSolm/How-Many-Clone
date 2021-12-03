using UnityEngine;

public class EndGameScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup _screenCanvasGroup;

    private bool _isFadeIn = false;
    private bool _isFadeOut = false;

    public void Show()
    {
        _isFadeIn = true;
    }

    public void Hide()
    {
        _isFadeOut = true;
    }

    private void Update()
    {
        if (_isFadeIn == true)
        {
            if (_screenCanvasGroup.alpha < 1)
            {
                _screenCanvasGroup.alpha += Time.deltaTime;

                if (_screenCanvasGroup.alpha >= 1)
                {
                    _isFadeIn = false;
                }
            }
        }

        if (_isFadeOut == true)
        {
            if (_screenCanvasGroup.alpha > 0)
            {
                _screenCanvasGroup.alpha -= Time.deltaTime;

                if (_screenCanvasGroup.alpha <= 0)
                {
                    _isFadeOut = false;
                }
            }
        }
    }
}
