using _GAME.Scripts.Controllers;
using _GAME.Scripts.Popup;
using DG.Tweening;
using GreiB.GameServices.Audio.Scripts;
using GreiB.UIManager.Scripts.Base;
using GreiB.UIManager.Scripts.UIPopup;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static PopupListProducts;
using static UnityEditor.Progress;

public class GameHud : MonoBehaviour
{
    [SerializeField] private GameObject prefTarget;
    [SerializeField] private Transform parenttarget;

    [SerializeField] private BuyItem buyItem;
    [SerializeField] private GameObject joystick;
    [SerializeField] private GameObject buttonSettings;

    [Space(2.0f)]
    [Header("TIME")]
    [SerializeField] private Text txtTimer;

    [Space(2.0f)]
    [Header("Money")]
    [SerializeField] private Text txtMoney;

    private LevelData levelData;

    //[SerializeField] private Joystick joystickMove;
    //[SerializeField] private Joystick joystickLook;

    private void Start()
    {
        this.buyItem.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        foreach (var item in _itemsTarget)
        {
            item.transform.SetParent(null);
            SimplePool.Despawn(item.gameObject);
        }
        _itemsTarget.Clear();
    }

    public void Set()
    {
        levelData = GameManager.Instance.GetCurrentLevelData();
        UpdateMoney(0, levelData.BudgetMoney, 1.0f);
        UpdateTimer(levelData.Timer);
        UpdateTarget();
    }

    public void HideAll()
    {
        joystick.SetActive(false);
        buyItem.SetActive(false);
        buttonSettings.SetActive(false);
    }

    public void ButtonSettings()
    {
        var param = new SettingPopupParam
        {
            showGroupBtn = true
        };
        AudioManager.Instance.PlaySfx(AudioName.UI_Click);
        UIManager.Instance.PopupManager.ShowPopup(UIPopupName.SettingPopup, param);
    }

    public void ShowBuyItem(Stall stall = null)
    {
        buyItem.SetActive(stall != null);
        if (stall != null)
        {
            buyItem.Set(stall, OnOpenProductListInStall);
        }
        else
        {
            buyItem.Reset();
        }
    }

    private void OnOpenProductListInStall(Stall stall)
    {
        if (stall.StallID == StallID.CashRegister)
        {
            //Calculate the bill
            GameController.Instance.CalculateTheBill();
        }
        else
        {
            //open UI
            UIManager.Instance.PopupManager.ShowPopup(UIPopupName.PopupListProducts, new PopupListProductsParam { stall = stall });
        }
    }

    #region MONEY
    public void UpdateMoney(int current, int money, float duration)
    {
        DOTween.To(() => current, x =>
        {
            current = x;
            this.txtMoney.text = $"${current}";
        }, money, duration);
    }
    #endregion

    #region TIME
    public void UpdateTimer(float timer)
    {
        TimeSpan ts = TimeSpan.FromSeconds(timer);
        string formatted = string.Format("{0:D2}:{1:D2}", ts.Minutes, ts.Seconds);
        this.txtTimer.text = $"{formatted}";
    }
    #endregion

    #region TARGET
    private List<TargetItemInGame> _itemsTarget = new List<TargetItemInGame>();
    private void UpdateTarget()
    {
        foreach (var tg in levelData.TargetStalls)
        {
            var pref = SimplePool.Spawn(prefTarget, Vector3.zero, Quaternion.identity);
            var item = pref.GetComponent<TargetItemInGame>();
            if (item != null)
            {
                item.transform.SetParent(parenttarget);
                item.gameObject.transform.localScale = Vector3.one;
                item.Set(tg);
                _itemsTarget.Add(item);
            }
        }

        foreach (var tg in levelData.TargetProducts)
        {
            var pref = SimplePool.Spawn(prefTarget, Vector3.zero, Quaternion.identity);
            var item = pref.GetComponent<TargetItemInGame>();
            if (item != null)
            {
                item.transform.SetParent(parenttarget);
                item.gameObject.transform.localScale = Vector3.one;
                item.Set(tg);
                _itemsTarget.Add(item);
            }
        }
    }

    public void UpdateTarget(ProductID productID, int quantity)
    {
        var item = _itemsTarget.Find(x => x.TargetProduct?.productID == productID);
        if (item != null)
        {
            Debug.Log($"Update product: {quantity}/{item.TargetProduct.amount}");
            item.UpdateQuantityTarget(quantity);
        }
    }

    public void UpdateTarget(StallID stallID, int quantity)
    {
        var item = _itemsTarget.Find(x => x.TargetStall?.stallID == stallID);
        if (item != null)
        {
            Debug.Log($"Update stall: {quantity}/{item.TargetStall.amount}");
            item.UpdateQuantityTarget(quantity);
        }
    }

    public bool IsCompletedAllTarget()
    {
        var item = _itemsTarget.Find(x => !x.IsCompleted());
        return item == null;
    }
    #endregion
}