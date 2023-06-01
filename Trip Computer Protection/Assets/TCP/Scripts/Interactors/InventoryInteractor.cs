using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryInteractor : Interactor
{
    public int Money { get; private set; }
    public int Supplies { get; private set; }
    int startmoney = 1000;
    int startSupplies = 200;

    public override void OnStart()
    {
        Money = startmoney;
        Supplies = startSupplies;
    }

    public bool SpendMoney(int amount)
    {
        if(Money < amount) return false;
        else { Money -= amount; Debug.Log(Money); return true; }
    }

    public void AddMoney(int amount)
    {
        Money += amount;
    }

    public void AddSupplies(int amount)
    {
        Supplies += amount;
        Debug.Log("+ " + Supplies);
    }

    public bool SpendSupplies(int amount)
    {
        if (Supplies < amount) return false;
        else { Supplies -= amount; Debug.Log(Supplies); return true; }
    }
}
