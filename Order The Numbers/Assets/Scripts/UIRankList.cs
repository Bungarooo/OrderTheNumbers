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

    void Awake()
    {
        rank = this.transform.GetSiblingIndex();
        rankText = this.transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>();
        rankText.text = (rank + 1).ToString();
        valueText = this.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        valueText.text = "---";
    }

    void SynchronizeValue(int[] solutionArr)
    {
        if (solutionArr[rank] == -1) return;

        valueText.text = solutionArr[rank].ToString();
    }

    void SynchronizeButtonEnabled(int generatedNumber)
    {
        this.transform.GetChild(0).GetComponent<Button>().interactable = 
            GameMaster.Instance().GetSolutionArrayFromIndex(rank) == -1 && GameMaster.Instance().EvaluateLegality(rank, generatedNumber);
    }

    void OnEnable()
    {
        GameMaster.Instance().OnSolutionUpdated += SynchronizeValue;
        GameMaster.Instance().OnNumberGenerated += SynchronizeButtonEnabled;
    }

    void OnDisable()
    {
        GameMaster.Instance().OnSolutionUpdated -= SynchronizeValue;
        GameMaster.Instance().OnNumberGenerated -= SynchronizeButtonEnabled;
    }

    public void Clicked()
    {
        GameMaster.Instance().ButtonPressed(rank);
        //this.transform.GetChild(0).GetComponent<Button>().enabled = (GameMaster.Instance().GetSolutionArrayFromIndex(rank) == -1);
    }
}
