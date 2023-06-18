using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIRankList: MonoBehaviour
{
    int rank;
    TMP_Text rankText;
    TMP_Text valueText;
    Button button;

    void Awake()
    {
        rank = this.transform.GetSiblingIndex();
        rankText = this.transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>();
        valueText = this.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        button = this.transform.GetChild(0).GetComponent<Button>();
        Init();
    }

    void Init()
    {
        rankText.text = (rank + 1).ToString();
        valueText.text = "---";
        button.interactable = true;
    }

    void SynchronizeValue(int[] solutionArr)
    {
        if (solutionArr[rank] == -1) return;

        valueText.text = solutionArr[rank].ToString();
    }

    void SynchronizeButtonEnabled(int generatedNumber)
    {
        button.interactable = GameMaster.Instance().GetSolutionArrayFromIndex(rank) == -1 && GameMaster.Instance().EvaluateLegality(rank, generatedNumber);
    }

    void OnEnable()
    {
        GameMaster.Instance().OnSolutionUpdated += SynchronizeValue;
        GameMaster.Instance().OnNumberGenerated += SynchronizeButtonEnabled;
        Initializer.Instance.OnInitialize += Init;
    }

    void OnDisable()
    {
        GameMaster.Instance().OnSolutionUpdated -= SynchronizeValue;
        GameMaster.Instance().OnNumberGenerated -= SynchronizeButtonEnabled;
        Initializer.Instance.OnInitialize -= Init;
    }

    public void Clicked()
    {
        GameMaster.Instance().ButtonPressed(rank);
    }
}
