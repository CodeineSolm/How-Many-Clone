using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlagsLayout : MonoBehaviour
{
    [SerializeField] private FlagView _template;
    [SerializeField] private Transform _container;
    [SerializeField] private List<Flag> _flags;
    [SerializeField] private ChooseFlagView _chooseFlagView;
    [SerializeField] private Image _chooseButtonFlagImage;

    private FlagView _view;

    private void Awake()
    {
        Render(_flags);
    }

    private void Render(IEnumerable<Flag> flags)
    {
        foreach (var flag in flags)
        {
            _view = Instantiate(_template, _container) as FlagView;
            _view.Render(flag);
            _view.FlagViewClicked += OnFlagViewClicked;
        }
    }

    private void OnDestroy()
    {
        _view.FlagViewClicked -= OnFlagViewClicked;
    }

    private void OnFlagViewClicked(Image _flagImage)
    {
        _chooseButtonFlagImage.sprite = _flagImage.sprite;
        _chooseFlagView.gameObject.SetActive(false);
    }
}
