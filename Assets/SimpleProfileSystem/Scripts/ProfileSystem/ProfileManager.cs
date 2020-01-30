using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ProfileManager выступает как некий общий контроллер, 
/// который имеет и делает слабосвязанными более мелкие контроллеры, 
/// ну и упраляет общей логикой работы с ProfileSettings
/// </summary>
public class ProfileManager : MonoBehaviour
{
    #region Private SerializeFields
    //Сериализованное поле ProfileSettings 
    [SerializeField]
    private ProfileSettings _profileSettings;
    //Сериализованное поле ProfileManagerPresentor
    [SerializeField]
    private ProfileManagerPresentor _profileManagerPresentor;
    #endregion

    
    #region Prifate Fields
    //Поле PlayerSelector, выступает как более мелкий контроллер
    private PlayerSelector _playerSelector;
    #endregion

    private void Awake()
    {
        //Проверяем, задан ли мы в испекторе Profile Settings и если нет, то бросаем Исключение
        if (_profileSettings == null)
        {
            throw new NullReferenceException($"{nameof(ProfileSettings)} не задан, пожалуйста укажите его и перезапустите игру");
        }
        
        //Загружаем данные из диска
        _profileSettings.LoadData();
        //Тут инициализирум более мелкие контроллеры и презентор...

        //Присваиваем значения из настроек свойствам презентора
        _profileManagerPresentor.Cash = _profileSettings.Cash;
        _profileManagerPresentor.Record = _profileSettings.Record;
        //Инициализируем PlayerSelector
        InitPlayerSelector();

        //Конец инициализации

        //Сохраняем данные, тк при той же самой инициализации контроллеров, могут измениться данные
        _profileSettings.SaveData();
    }

    private void OnDisable()
    {
        //При Отключении объекта отписываем слушателя OnSelectPlayer от события OnSelectChange в контроллере _playerSelector
        _playerSelector.OnSelectChange.RemoveListener(OnSelectPlayer);
    }
    /// <summary>
    /// Запуск игры
    /// </summary>
    public void Play()
    {
        Debug.Log("Play");
    }
    /// <summary>
    /// Инициализируем PlayerSelector
    /// </summary>
    private void InitPlayerSelector()
    {
        //Проверяем, задан ли хотя-бы один элемент в коллекции Players и если нет, то бросаем Исключение
        if (_profileSettings.Players == null)
        {
            throw new NullReferenceException($"В {nameof(ProfileSettings)}, коллекция {nameof(ProfileSettings.Players)} равна null, пожалуйста создайте хотя-бы один элемент");
        }
        //Проверяем есть ли у объекта в котором находится компонент ProfileManager - компонент PlayerSelector
        //Если нет, то бросаем исключение 
        //Если есть, то присваимаем _playerSelector ссылку на него
        if (!TryGetComponent(out PlayerSelector playerSelector))
        {
            throw new NullReferenceException($"В {gameObject.name} нет компонента {nameof(PlayerSelector)}. Пожалуйста установите его");
        }
        else
        {
            _playerSelector = playerSelector;
        }
        //Подписываем слушателя OnSelectPlayer для события OnSelectChange в контроллере _playerSelector
        _playerSelector.OnSelectChange.AddListener(OnSelectPlayer);
        //Инициализируем _playerSelector
        _playerSelector.InitPlayerSelector(_profileSettings.CurrentPlayer, _profileSettings.Players);
    }
    /// <summary>
    /// Метод слушателя для события OnSelectChange в контроллере _playerSelector
    /// </summary>
    /// <param name="player">Параметер PlayerEntity, который передается сюда при вызове события OnSelectChange в контроллере _playerSelector</param>
    private void OnSelectPlayer(PlayerEntity player)
    {
        //Присваиваем параметр player к CurrentPlayer в настройках
        _profileSettings.CurrentPlayer = player;
        //Сохраняем данные
        _profileSettings.SaveData();
    }

    
}
