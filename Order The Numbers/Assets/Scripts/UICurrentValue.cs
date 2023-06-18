using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICurrentValue : MonoBehaviour
{
    TMP_Text text;

    void Awake()
    {
        text = this.GetComponent<TMP_Text>();
    }

    void SynchronizeUI(int generatedNumber)
    {
        text.text = generatedNumber.ToString();
    }

    void OnEnable()
    {
        GameMaster.Instance().OnNumberGenerated += SynchronizeUI;
        SynchronizeUI(GameMaster.Instance().GetGeneratedNumber());
    }

    void OnDisable()
    {
        GameMaster.Instance().OnNumberGenerated -= SynchronizeUI;
    }
}
