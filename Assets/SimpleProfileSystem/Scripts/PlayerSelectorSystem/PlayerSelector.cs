using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerSelector : MonoBehaviour
{
    #region Public Fields
    public SelectPlayerEvent OnSelectChange = new SelectPlayerEvent();
    #endregion

    #region Private SerializeFields
    [SerializeField]
    private Transform _transformContainerPlayers; 
    [SerializeField]
    private PlayerSelectorPresentor _playerSelectorPresentor;
    [SerializeField]
    private List<GameObject> _instantiatedPlayers;
    [SerializeField]
    private GameObject _selectedPlayer;
    #endregion

    #region Private Fields
    private List<PlayerEntity> _players;
    private PlayerEntity _currentPlayer;
    #endregion

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
    private void SelectPlayer(PlayerEntity player)
    {
        if (player == null)
        {
            throw new ArgumentNullException($"{nameof(player)}", $"Аргумент {nameof(player)} равен null. Пожалуйста передайте правильный аргумент!");
        }
        if (_selectedPlayer != null)
        {
            _selectedPlayer.SetActive(false);
        }
        int indexPlayer = _players.IndexOf(player);
        _currentPlayer = player;
        OnSelectChange.Invoke(_currentPlayer);
        _selectedPlayer = _instantiatedPlayers[indexPlayer];
        _selectedPlayer.SetActive(true);
        _playerSelectorPresentor.NamePlayer = _currentPlayer.Name;
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
[Serializable]
public class SelectPlayerEvent : UnityEvent<PlayerEntity> { }
