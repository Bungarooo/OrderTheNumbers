using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGameOverPage : MonoBehaviour
{
    GameObject panel;
    TMP_Text text;
    void Awake()
    {
        panel = this.transform.GetChild(0).gameObject;
        text = this.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
    }

    void SetUpWinPanel()
    {
        text.text = "Perfect Score!\n" + GameMaster.Instance().GetCurrentTurn() + " Points";
        panel.SetActive(true);
    }

    void SetUpLosePanel()
    {
        text.text = "Your Score\n" + (GameMaster.Instance().GetCurrentTurn() - 1) + " Points";
        panel.SetActive(true);
        
    }

    void DisablePanel()
    {
        panel.SetActive(false);
    }

    void OnEnable()
    {
        GameMaster.Instance().OnWin += SetUpWinPanel;
        GameMaster.Instance().OnLose += SetUpLosePanel;
        Initializer.Instance.OnInitialize += DisablePanel;
    }

    void OnDisable()
    {
        GameMaster.Instance().OnWin -= SetUpWinPanel;
        GameMaster.Instance().OnLose -= SetUpLosePanel;
        Initializer.Instance.OnInitialize -= DisablePanel;
    }
}
