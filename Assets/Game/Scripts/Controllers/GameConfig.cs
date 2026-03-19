using Imba.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum StallID
{
    CashRegister,
    Fruits,

    Vegetable,
    DrinkBoxes,
    Dinks,
    Freeze,

    FastFood,
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
    Lemon,

    //Vegetable
    Egg,
    Tomatoes,
    Mushroom,
    Cucumber,
    Bell_pepper,
    Green_beans,
    Lettuce,
    Cabbage,
    Green_spinach,
    Carrot
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
}

public class GameConfig : ManualSingletonMono<GameConfig>
{
    public int MaxItemInShoppingCart = 3;
    public List<StallProduct> ListStallInStore = new List<StallProduct>();
}
