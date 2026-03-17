using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelData", menuName = "Scriptable Objects/Level Data")]
public class LevelData : ScriptableObject
{
    public string Name;
    public int Level;
    public int BudgetMoney;
    public int Vat;
    public List<TargetLevel> TargetLevel = new List<TargetLevel>();
    public float Timer;
    public int Sale; //Storewide sale - sale toan cua hang
    public List<StallID> StallIDs = new(); //Luu nhung id prefab se create trong level do
    public List<SaleProduct> SaleProducts = new();
}

[Serializable]
public class TargetLevel
{
    public StallID stallID;
    public int amount;
}

[Serializable]
public class SaleProduct
{
    public ProductID productID;
    public float sale;
}