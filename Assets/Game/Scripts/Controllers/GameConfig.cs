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
    Bread,

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
    Carrot,
    Okra,
    Hanhla,
    Hanhtay,

    //Drink box
    FermentedMilk,
    Milk,
    SoyMilk,
    AppleJuice,
    GrapeJuice,
    MangoJuice,
    MatchaTea,
    OrangeMilk,
    StrawberryMilk,

    //Freeze
    Xucxich,
    Cahoi,
    Thitbo,
    Thitbocatlat,
    Thitbosteak,
    Ga,
    Ca,
    Canhga,
    Duiga,
    Raucu,

    //Drinks
    MineralWater,
    Coke,
    Sprite,
    CoffeeCan,
    OrangeCan,
    OrangeSoda,
    LemonadeSoda,
    LemonCan,
    SodaCan,
    Sake,

    //Eat
    Banhmi_1,
    Banhmi_2,
    Comnam,
    Comcuon,
    Pizza,
    Sushi_1,
    Sushi_2,
    Sushi_3,
    Sushi_4,
    Sushi_5,
    Sushi_6,
    Sushi_7,
    Sushi_8,
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
