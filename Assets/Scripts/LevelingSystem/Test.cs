using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public bool A, B, C, D;
    int upgradeTimes;
    int coins;

    private void Start()
    {
        upgradeTimes = 0;
        coins = 0;
    }
    private void Update()
    {
        UpgradeSignal();
    }
    int UpgradeSignal()
    {
        if (A)
        { upgradeTimes *= 5; Debug.Log("5"); }
        if (B)
        { upgradeTimes *= 10; Debug.Log("10"); }
        if (C)
        { upgradeTimes *= 50; Debug.Log("50"); }
        if (D)
        { upgradeTimes *= 100;
            Debug.Log("100");
        }

        return upgradeTimes;
    }
    bool CheckConditions()
    {

        return true;
    }

    void ParameterUpGrade()
    {

    }
}
