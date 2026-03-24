using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelData", menuName = "Scriptable Objects/Level Data")]
public class LevelData : ScriptableObject
{
    public int Level;
    public int BudgetMoney;
    public int Vat;
    public List<TargetStall> TargetStalls = new();
    public List<TargetProduct> TargetProducts = new();
    public float Timer;
    public int Sale; //Storewide sale - sale toan cua hang
    public List<StallID> StallIDs = new(); //Luu nhung id prefab se create trong level do??
    public List<SaleProduct> SaleProducts = new();
    public List<TargetProduct> results = new();

    public SaleProduct IsSaleForStall(StallID stallId)
    {
        var sale = SaleProducts.Find(x => x.stallId == stallId);
        return sale;
    }
}

[Serializable]
public class TargetStall
{
    public StallID stallID;
    public int amount;
}

[Serializable]
public class TargetProduct
{
    public ProductID productID;
    public int amount;
}

[Serializable]
public class SaleProduct
{
    public StallID stallId;
    public float sale;
}