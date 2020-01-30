using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Презентор PlayerSelector
/// </summary>
[Serializable]
public class PlayerSelectorPresentor
{
    /// <summary>
    /// Свойство NamePlayer, с блоками get и set, который получает нынешнюю строку имени, а также устанавливает новую
    /// </summary>
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

    //Сериализованные поля с различными UI компонентами, необхадимыми для представления
    [SerializeField]
    private Image _imageForeground;
    [SerializeField]
    private Image _imageUnavailable;
    [SerializeField]
    private Text _textNamePlayer;

    //Сериализованные поля Color для компонента _imageForeground
    [Header("Foreground Colors")]
    [SerializeField]
    private Color _colorUnavailable;
    [SerializeField]
    private Color _colorAvailable;
    //Поле с именем игрока
    private string _namePlayer;
    /// <summary>
    /// Устанавливает представление, как если бы Player будет доступен
    /// </summary>
    public void Available()
    {
        //Устанавливаем Цвет для компонета _imageForeground
        _imageForeground.color = _colorAvailable;
        //Выключаем объект _imageUnavailable
        _imageUnavailable.gameObject.SetActive(false);
    }
    /// <summary>
    /// Устанавливает представление, как если бы Player будет недоступен
    /// </summary>
    public void Unavailable()
    {
        //Устанавливаем Цвет для компонета _imageForeground
        _imageForeground.color = _colorUnavailable;
        //Включаем объект _imageUnavailable
        _imageUnavailable.gameObject.SetActive(true);
    }
}
