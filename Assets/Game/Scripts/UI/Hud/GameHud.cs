using _GAME.Scripts.Controllers;
using _GAME.Scripts.Popup;
using GreiB.GameServices.Audio.Scripts;
using GreiB.UIManager.Scripts.Base;
using GreiB.UIManager.Scripts.UIPopup;
using System;
using UnityEngine;
using UnityEngine.UI;
using static PopupListProducts;

public class GameHud : MonoBehaviour
{
    [SerializeField] private BuyItem buyItem;
    [SerializeField] private GameObject joystick;

    [Space(2.0f)]
    [Header("TIME")]
    [SerializeField] private Text txtTimer;

    [Space(2.0f)]
    [Header("Money")]
    [SerializeField] private Text txtMoney;

    //[SerializeField] private Joystick joystickMove;
    //[SerializeField] private Joystick joystickLook;

    private void Start()
    {
        this.buyItem.gameObject.SetActive(false);
    }

    private void Set()
    {
        var data = GameManager.Instance.GetCurrentLevelData();
        UpdateMoney(data.BudgetMoney);
        UpdateTimer(data.Timer);
    }

    public void HideAll()
    {
        joystick.SetActive(false);
        buyItem.SetActive(false);
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
        } else
        {
            //open UI
            UIManager.Instance.PopupManager.ShowPopup(UIPopupName.PopupListProducts, new PopupListProductsParam { stall = stall });
        }
    }

    #region MONEY
    public void UpdateMoney(int money)
    {
        this.txtMoney.text = $"${money}";
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
}