using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyItem : MonoBehaviour
{
    [SerializeField] private Text nameItem;
    private Stall stall;
    private Action<Stall> OnClicked = null;

    public void Reset()
    {
        this.stall = null;
        this.OnClicked = null;
        RefreshView();
    }

    public void Set(Stall stall, Action<Stall> OnClicked)
    {
        this.stall = null;
        this.OnClicked = OnClicked;
        RefreshView();
    }

    private void RefreshView()
    {
        if(stall != null)
        {
            this.nameItem.text = name;
        } else
        {
            this.nameItem.text = "N/A";
        }
    }

    private void OnBuyItem()
    {
        if (stall == null) return;
        OnClicked?.Invoke(stall);
    }
}
