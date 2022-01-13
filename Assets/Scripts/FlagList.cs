using System.Collections.Generic;
using UnityEngine;

public class FlagList : MonoBehaviour
{
    [SerializeField] private List<Sprite> _flags;

    public Sprite GetFlag()
    {
        return _flags[Random.Range(0, _flags.Count)];
    }
}
