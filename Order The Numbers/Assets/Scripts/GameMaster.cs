using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster
{
    private static GameMaster _instance;
    private static readonly object _lock = new object();
    public static GameMaster Instance()
    {
        if(_instance == null)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new GameMaster();
                }
            }
            
        }

        return _instance;
    }

    public GameMaster() {
        currentTurn = 0;
        generatedNumber = -1;
        turns = 7;
        solution = new int[turns];

        for(int i = 0; i < solution.Length; i++)
        {
            solution[i] = -1;
        }

        GameLoop();
    }

    int currentTurn;
    int generatedNumber;
    int turns;
    public event Action<int> OnNumberGenerated;
    public event Action<int[]> OnSolutionUpdated;
    int[] solution;

    public void GameLoop()
    {
        if (currentTurn < turns)
        {
            currentTurn++;
            generatedNumber = UnityEngine.Random.Range(0, 1000);
            OnNumberGenerated?.Invoke(generatedNumber);
            if(AnyLegalMovesRemaining(generatedNumber) == false)
            {
                Lose();
            }
        }
        else
        {
            Win();
        }
    }

    public int GetGeneratedNumber() { return generatedNumber; }
    public int GetSolutionArrayFromIndex(int index) { return solution[index]; }

    public void ButtonPressed(int buttonIndex)
    {
        if (EvaluateLegality(buttonIndex, generatedNumber))
        {
            solution[buttonIndex] = generatedNumber;
            OnSolutionUpdated?.Invoke(solution);
            GameLoop();
        }

    }

    public bool EvaluateLegality(int index, int newValue)
    {
        int trav = index - 1;

        while (trav >= 0)
        {
            if (solution[trav] == -1)
            {
                trav--;
                continue;
            }
            if (solution[trav] <= newValue)
            {
                break;
            }
            else
            {
                return false;
            }
        }

        trav = index + 1;
        while (trav < solution.Length)
        {
            if (solution[trav] == -1)
            {
                trav++;
                continue;
            }
            if (solution[trav] >= newValue)
            {
                break;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    bool AnyLegalMovesRemaining(int newValue)
    {
        for(int i = 0; i < solution.Length; i++)
        {
            if (solution[i] == -1 && EvaluateLegality(i, newValue)) return true;
        }

        return false;
    }

    void Lose()
    {
        Debug.Log("You Lose | Your score: " + (currentTurn-1));
    }

    void Win()
    {
        Debug.Log("You Win! | Your score: " + currentTurn);
    }
}
