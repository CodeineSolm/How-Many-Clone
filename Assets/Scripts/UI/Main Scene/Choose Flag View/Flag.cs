using System;
using UnityEngine;

[Serializable]
public class Flag
{
    [SerializeField] private Sprite _image;

    public Sprite Image => _image;

    public Flag(Sprite image)
    {
        _image = image;
    }
}
