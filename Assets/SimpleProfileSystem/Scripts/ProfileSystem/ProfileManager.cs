using System;
using UnityEngine;

public class ProfileManager : MonoBehaviour
{
    #region Private SerializeFields
    [SerializeField]
    private ProfileSettings _profileSettings;
    [SerializeField]
    private ProfileManagerPresentor _profileManagerPresentor;
    #endregion

    
    #region Prifate Fields
    private PlayerSelector _playerSelector;
    #endregion

    private void Awake()
    {
        if (_profileSettings == null)
        {
            throw new NullReferenceException($"{nameof(ProfileSettings)} не задан, пожалуйста укажите его и перезапустите игру");
        }
        
        _profileSettings.LoadData();

        _profileManagerPresentor.Cash = _profileSettings.Cash;
        _profileManagerPresentor.Record = _profileSettings.Record;
        InitPlayerSelector();

        _profileSettings.SaveData();
    }

    private void OnDisable()
    {
        _playerSelector.OnSelectChange.RemoveListener(OnSelectPlayer);
    }
    public void Play()
    {
        Debug.Log("Play");
    }
    private void InitPlayerSelector()
    {
        if (_profileSettings.Players == null)
        {
            throw new NullReferenceException($"В {nameof(ProfileSettings)}, коллекция {nameof(ProfileSettings.Players)} равна null, пожалуйста создайте хотя-бы один элемент");
        }

        if (!TryGetComponent(out PlayerSelector playerSelector))
        {
            throw new NullReferenceException($"В {gameObject.name} нет компонента {nameof(PlayerSelector)}. Пожалуйста установите его");
        }
        else
        {
            _playerSelector = playerSelector;
        }
        _playerSelector.OnSelectChange.AddListener(OnSelectPlayer);
        _playerSelector.InitPlayerSelector(_profileSettings.CurrentPlayer, _profileSettings.Players);
    }
    
    private void OnSelectPlayer(PlayerEntity player)
    {
        _profileSettings.CurrentPlayer = player;
        _profileSettings.SaveData();
    }

    
}
