using _GAME.Scripts.Controllers;
using DG.Tweening;
using GreiB.UIManager.Scripts.UIPopup;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PopupCaculateBill : UIPopup
{
    [SerializeField] private GameObject _prefBillItem;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private GameObject container;
    [SerializeField] private Text _txtPrice;
    private List<BillItem> _items = new();
    private LevelData LevelData;
    private float totalPrice = 0;

    protected override void OnShowing()
    {
        base.OnShowing();

        totalPrice = 0;
        _items.Reverse();
        _items.ForEach(item => SimplePool.Despawn(item.gameObject));

        //param = (PopupLevelTargetParam)Parameter;
        LevelData = GameManager.Instance.GetCurrentLevelData();
        var list = GameController.Instance.LstProdutsInCart;
        title.SetActive(false);
        bot.SetActive(true);
        scrollRect.gameObject.SetActive(true);
        _txtPrice.gameObject.SetActive(true);
        container.SetActive(false);
        foreach (var product in list)
        {
            var pref = SimplePool.Spawn(_prefBillItem, Vector3.zero, Quaternion.identity);
            var item = pref.GetComponent<BillItem>();
            if (item != null)
            {
                item.transform.SetParent(scrollRect.content);
                item.gameObject.transform.localScale = Vector3.one;
                item.Set(product);
                _items.Add(item);
            }
        }

        _ = CaculatePriceAsync();
    }

    private async Task CaculatePriceAsync()
    {
        foreach (var item in _items)
        {
            var price = item.GetPrice();
            totalPrice += price;
            await Task.Delay(2000);
            SetPrice();
            //await Task.Delay(1000);
            //SimplePool.Despawn(item.gameObject);
        }
    }

    private void SetPrice()
    {
        //float rounded = Mathf.Round(totalPrice * 10f) / 10f;
        //_txtPrice.text = $"{rounded.ToString("0.0")}";
        _txtPrice.text = $"{totalPrice.ToString("0.0")}";
    }

    [Space(5.0f)]
    [Header("PRICE")]
    [SerializeField] private GameObject bot;
    [SerializeField] private GameObject title;
    [SerializeField] private Text txtTotal;
    [SerializeField] private Text txtDiscountPercent;
    [SerializeField] private Text txtDiscount;
    [SerializeField] private Text txtVatPercent;
    [SerializeField] private Text txtVat;
    [SerializeField] private Text txtGrandTotal;
    public void Summary()
    {
        title.SetActive(true);
        bot.SetActive(false);
        _txtPrice.gameObject.SetActive(false);
        scrollRect.gameObject.SetActive(false);
        container.SetActive(true);

        txtTotal.text = $"{totalPrice.ToString("0.0")}";

        txtDiscountPercent.text = $"Discount ({LevelData.Sale}%)";
        float discount = totalPrice * (LevelData.Sale / 100f);
        txtDiscount.text = $"{discount.ToString("0.0")}";

        txtVatPercent.text = $"VAT ({LevelData.Vat}%)";
        float vatAmount = totalPrice * (LevelData.Vat / 100f);
        txtVat.text = $"{vatAmount.ToString("0.0")}";

        var grandTotal = totalPrice - discount + vatAmount;
        //txtGrandTotal.text = $"{grandTotal}";

        float currentValue = 0f;
        DOTween.To(() => currentValue, x => {
            currentValue = x;
            txtGrandTotal.text = currentValue.ToString("0.0"); // format 0.0
        }, grandTotal, 1.5f);
    }


}