using System;
using UnityEngine;

[Serializable]
public class PlayerEntity
{
    #region Public Properties
    public string Name => _name;
    public GameObject Prefab => _prefab;
    public bool IsAvailable
    {
        get => _isAvailable;
        set => _isAvailable = value;
    }
    #endregion
    #region Private SerializeFields
    [SerializeField]
    private string _name;
    [SerializeField]
    private GameObject _prefab;
    [SerializeField]
    private bool _isAvailable;
    #endregion
}
