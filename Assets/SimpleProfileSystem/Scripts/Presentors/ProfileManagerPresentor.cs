using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Презентор ProfileManager
/// </summary>
[Serializable]
public class ProfileManagerPresentor
{
    /// <summary>
    /// Свойство Record с блоками get и set, необходим, чтобы устанавливать и обновлять текст в _textRecord
    /// </summary>
    public float Record
    {
        get => _record;
        set 
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException("Record", "Значение Record не может быть меньше 0");
            }
            _record = value;
            _textRecord.text = $"РЕКОРД: {_record}"; 
        }
    }
    /// <summary>
    /// Свойство Cash с блоками get и set, необходим, чтобы устанавливать и обновлять текст в _textCash
    /// </summary>
    public int Cash
    {
        get => _cash;
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException("Cash", "Значение Cash не может быть меньше 0");
            }
            _cash = value;
            _textCash.text = $"ДЕНЬГИ: {_cash}";
        }
    }

    //Сериализзованные поля с компонентами Text
    [SerializeField]
    private Text _textRecord;
    [SerializeField]
    private Text _textCash;

    //Контейнеры для свойств
    private float _record;
    private int _cash;
}
