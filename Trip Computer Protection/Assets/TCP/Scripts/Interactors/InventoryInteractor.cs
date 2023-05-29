using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryInteractor : Interactor
{
    public int Money { get; private set; }
    int startmoney = 1000;

    public override void OnStart()
    {
        Money = startmoney;
    }

    public bool SpendMoney(int count)
    {
        if(Money < count) return false;
        else { Money -= count; Debug.Log(Money); return true; }
    }
}
