using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// PlayerSelector выступает как контроллер, который отвечает за выбор игрока
/// </summary>
public class PlayerSelector : MonoBehaviour
{
    #region Public Fields
    //Событие OnSelectChange
    public SelectPlayerEvent OnSelectChange = new SelectPlayerEvent();
    #endregion

    #region Private SerializeFields
    //Transform с контейнером, куда инициализируются все Игроки(объекты)
    [SerializeField]
    private Transform _transformContainerPlayers; 
    //Презентор PlayerSelector
    [SerializeField]
    private PlayerSelectorPresentor _playerSelectorPresentor;
    //Коллекция с созданными Игроками(объектами)
    [SerializeField]
    private List<GameObject> _instantiatedPlayers;
    //Выбранный на данный момент Игрок(объект)
    [SerializeField]
    private GameObject _selectedPlayer;
    #endregion

    #region Private Fields
    //Коллекция сущностей игроков
    private List<PlayerEntity> _players;
    //Сущность игрока, которая используется на данный момент
    private PlayerEntity _currentPlayer;
    #endregion

    /// <summary>
    /// Инициализация PlayerSelector
    /// </summary>
    /// <param name="currentPlayer">Передаем сюда из настроек CurrentPlayer</param>
    /// <param name="players">Передаем сюда из настроек Players</param>
    public void InitPlayerSelector(PlayerEntity currentPlayer, List<PlayerEntity> players)
    {
        //Проверяем равна ли коллекция players - null и если да, то выбрасываем исключение
        if (players == null)
        {
            throw new ArgumentNullException(
                $"{nameof(players)}",
                $"Аргумент {nameof(players)} не задан, пожалуйста проверьте настройки");
        }
        //Проверяем установлен ли TransformContainerPlayers и если нет, то выбрасываем исключение
        if (_transformContainerPlayers == null)
        {
            throw new NullReferenceException("У вас не установлен TransformContainerPlayers! Пожалуйста установите его");
        }
        //Присваиваем ссылку на players, коллекции _players
        _players = players;
        //Перебираем каждый элемент коллекции _players
        foreach (var player in _players)
        {
            //Создаем новый объект
            GameObject instantiatedPlayer = Instantiate(player.Prefab, _transformContainerPlayers);
            //Выключаем его
            instantiatedPlayer.SetActive(false);
            //Добавляем в коллекцию с созданными игроками
            _instantiatedPlayers.Add(instantiatedPlayer);
            //Проверям если этот элемент равен параметру currentPlayer, то присваимаем его к _currentPlayer
            if (player == currentPlayer)
            {
                _currentPlayer = player;
            }
        }
        //Проверяем не равен ли _currentPlayer - null и если нет, то Выбираем этот объект, иначе Выбираем первый элемент из коллекции
        if (_currentPlayer != null)
        {
            SelectPlayer(_currentPlayer);
        }
        else
        {
            SelectPlayer(_players.First());
        }
    }
    /// <summary>
    /// Движение элементов вправо
    /// </summary>
    public void MoveRightSelect()
    {
        //Получаем индекс выбранного игрока
        int currentPlayerIndex = _instantiatedPlayers.IndexOf(_selectedPlayer);
        //Проверяем, если инекс == последниму индексу в коллекции, то выбираем первый элемент коллекции, иначе двигаемся вправо
        if (currentPlayerIndex == _instantiatedPlayers.Count - 1)
        {
            SelectPlayer(_players.First());
        }
        else
        {
            currentPlayerIndex++;
            SelectPlayer(_players[currentPlayerIndex]);
        }
    }
    /// <summary>
    /// Движение элементов влево
    /// </summary>
    public void MoveLeftSelect()
    {
        //Получаем индекс выбранного игрока
        int currentPlayerIndex = _instantiatedPlayers.IndexOf(_selectedPlayer);
        //Проверяем, если инекс == первому индексу в коллекции, то выбираем последний элемент коллекции, иначе двигаемся влево
        if (currentPlayerIndex == 0)
        {
            SelectPlayer(_players.Last());
        }
        else
        {
            currentPlayerIndex--;
            SelectPlayer(_players[currentPlayerIndex]);
        }

    }
    /// <summary>
    /// Выбор игрока
    /// </summary>
    /// <param name="player">Передаем сюда элемент коллекции _players</param>
    private void SelectPlayer(PlayerEntity player)
    {
        //Проверяем, если параметр равен null, то выбрасываем исключение
        if (player == null)
        {
            throw new ArgumentNullException($"{nameof(player)}", $"Аргумент {nameof(player)} равен null. Пожалуйста передайте правильный аргумент!");
        }
        //Проверяем, если выбранный Игрок не равен null, то выключаем его
        if (_selectedPlayer != null)
        {
            _selectedPlayer.SetActive(false);
        }
        //Получаем индекс определенного элемента коллекции _players
        int indexPlayer = _players.IndexOf(player);
        //Присваиваем _currentPlayer ссылку на player
        _currentPlayer = player;
        //Вызываем событие OnSelectChange в который передаем параметр _currentPlayer
        OnSelectChange.Invoke(_currentPlayer);
        //Устанавливаем ссылку на нового игрока из коллекции созданных игроков
        _selectedPlayer = _instantiatedPlayers[indexPlayer];
        //Включаем объъект этого игрока
        _selectedPlayer.SetActive(true);
        //Устанавливаем Имя Игрока в презенторе
        _playerSelectorPresentor.NamePlayer = _currentPlayer.Name;
        //Проверяяем, если объъект доступен, то визуализируем его, как доступный, если нет, как не доступный
        if (_currentPlayer.IsAvailable)
        {
            _playerSelectorPresentor.Available();
        }
        else
        {
            _playerSelectorPresentor.Unavailable();
        }
    }

}
/// <summary>
/// Сериализованный класс, который наследуюется от UnityEvent<PlayerEntity>, 
/// тк Unity самостоятельно не может сериализовывать Generic типы
/// Необходим для создания событий 
/// </summary>
[Serializable]
public class SelectPlayerEvent : UnityEvent<PlayerEntity> { }
