using System.Collections.Generic;
using UnityEngine;

public class NamesList : MonoBehaviour
{
    [SerializeField] private List<string> _names;

    public string GetName()
    {
        return _names[Random.Range(0, _names.Count)];
    }
}
