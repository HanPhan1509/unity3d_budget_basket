using Imba.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum StallID
{
   Fruits,
   FastFood,
   Dinks,
   Cakes,
}

[Serializable]
public class Product
{
    public string id;
    public string Name;
    //public string Description;
    public int Price;
    public Sprite Preview;
}

[Serializable]
public class Stall
{
    public StallID StallID;
    public List<Product> Products;
    public Sprite Preview;
}

public class GameConfig : ManualSingletonMono<GameConfig>
{
    public int MaxItemInShoppingCart = 3;
}
