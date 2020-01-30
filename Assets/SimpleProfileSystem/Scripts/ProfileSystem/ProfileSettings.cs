using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ScriptableObject Настроек Профиля. Необходим, чтобы разделить ответсвенность между Моделью,Контролером,Представлением, 
/// но при этом используется как контейнер с различными параметрами, который делает слабосвязанными различные контроллеры
/// </summary>
[CreateAssetMenu(fileName = nameof(ProfileSettings), menuName = "Settings/ProfileSettings")]
public class ProfileSettings : ScriptableObject
{
    #region Public Fields
    //Публичные поля с настройками профиля
    public int Cash;
    public float Record;
    public List<PlayerEntity> Players = new List<PlayerEntity>();
    public PlayerEntity CurrentPlayer;
    #endregion
    /// <summary>
    /// Тут нужно реализовать логику загрузки данных Профиля
    /// </summary>
    public void LoadData()
    {
        //Загрузка
    }
    /// <summary>
    /// Тут нужно реализовать логику сохранения данных Профиля
    /// </summary>
    public void SaveData()
    {
        //Сохранение
    }
}
