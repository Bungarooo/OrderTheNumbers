using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    public static Initializer Instance;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        OnInitialize?.Invoke();
    }

    public event Action OnInitialize;

    public void Reinitialize()
    {
        OnInitialize?.Invoke();
    }
}
