using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ProfileManagerPresentor
{
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

    [SerializeField]
    private Text _textRecord;
    [SerializeField]
    private Text _textCash;

    private float _record;
    private int _cash;
}
