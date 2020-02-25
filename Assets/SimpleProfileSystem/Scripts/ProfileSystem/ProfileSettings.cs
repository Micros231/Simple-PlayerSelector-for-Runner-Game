using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(ProfileSettings), menuName = "Settings/ProfileSettings")]
public class ProfileSettings : ScriptableObject
{
    #region Public Fields
    public int Cash;
    public float Record;
    public List<PlayerEntity> Players = new List<PlayerEntity>();
    public PlayerEntity CurrentPlayer;
    #endregion
    public void LoadData()
    {
        //Загрузка
    }
    public void SaveData()
    {
        //Сохранение
    }
}
