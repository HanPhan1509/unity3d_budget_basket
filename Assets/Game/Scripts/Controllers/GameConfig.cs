using Imba.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum StallID
{
   CashRegister,

   Fruits,
   FastFood,
   Dinks,
   Cakes,
}

public enum ProductID
{
    //Fruits
    Apple,
    Strawberry,
    Orange,
    Banana,
    Kiwi_Slice,
    Grapes,
    Watermelon_Slice,
    Blue_Berry,
    Cherry,
    Pear,
    Lemon
}

[Serializable]
public class Product
{
    public ProductID Id;
    public int Price;
    //public Sprite Preview;
    public int Quantity;
    public int MaxQuantity = 5;
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
