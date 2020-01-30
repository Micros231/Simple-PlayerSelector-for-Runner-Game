using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Сериализованная сущность Игрока, которая нужна как контейнер с определенными свойствами для представления в игре
/// </summary>
[Serializable]
public class PlayerEntity
{
    #region Public Properties
    /// <summary>
    /// Свойство Name, c лямбда вызовом блока get. Необходимо, чтобы получить Имя игрока
    /// </summary>
    public string Name => _name;
    /// <summary>
    /// Свойство Prefab, c лямбда вызовом блока get. Необходимо, чтобы получить Префаб игрока
    /// </summary>
    public GameObject Prefab => _prefab;
    /// <summary>
    /// Свойство IsAvailable, с лямбда вызовом блоков get и set. 
    /// Необходимо, чтобы проверить, доступен для Игрок, 
    /// а также уставновить эту доступность
    /// (т.е к примеру при покупки, чтобы теперь он был доступен и имел другое представление в игре)
    /// </summary>
    public bool IsAvailable
    {
        get => _isAvailable;
        set => _isAvailable = value;
    }
    #endregion
    #region Private SerializeFields
    //Эти сериализованные поля, используютсся как контейнеры, а также нужны для правильного представления в Инспекторе Unity
    [SerializeField]
    private string _name;
    [SerializeField]
    private GameObject _prefab;
    [SerializeField]
    private bool _isAvailable;
    #endregion
}
