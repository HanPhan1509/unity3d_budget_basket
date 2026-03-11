using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelData", menuName = "Scriptable Objects/Level Data")]
public class LevelData : ScriptableObject
{
    public string Name;
    public int Level;
    public int BudgetMoney;
    public int vat;
    public List<ProductID> TargetProducts = new List<ProductID>();
    public float timer;
    public int sale;
    public List<StallID> StallIDs = new(); //Luu nhung id prefab se create trong level do
}