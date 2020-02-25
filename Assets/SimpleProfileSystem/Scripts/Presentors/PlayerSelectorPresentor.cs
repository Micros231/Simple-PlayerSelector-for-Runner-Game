using System;
using UnityEngine;
using UnityEngine.UI;
[Serializable]
public class PlayerSelectorPresentor
{
    public string NamePlayer
    {
        get => _namePlayer;
        set
        {
            //Првоеряем, равно ли переданное значение Имени игрока - null или просто пустое
            //если да, то уставнавливаем в качестве имени игрока "У персонажа нету имени"
            //если нет, то устанавливаем значение
            if (string.IsNullOrEmpty(value))
            {
                _namePlayer = "У персонажа нету имени";
            }
            else
            {
                _namePlayer = value;
            }
            //Присваиваем свойству text компонента _textNamePlayer имя игрока
            _textNamePlayer.text = _namePlayer;
        }
    }

    [SerializeField]
    private Image _imageForeground;
    [SerializeField]
    private Image _imageUnavailable;
    [SerializeField]
    private Text _textNamePlayer;
    [Header("Foreground Colors")]
    [SerializeField]
    private Color _colorUnavailable;
    [SerializeField]
    private Color _colorAvailable;

    private string _namePlayer;

    public void Available()
    {
        _imageForeground.color = _colorAvailable;
        _imageUnavailable.gameObject.SetActive(false);
    }
    public void Unavailable()
    {
        _imageForeground.color = _colorUnavailable;
        _imageUnavailable.gameObject.SetActive(true);
    }
}
