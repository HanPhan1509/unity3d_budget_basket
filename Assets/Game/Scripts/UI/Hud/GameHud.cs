using _GAME.Scripts.Controllers;
using _GAME.Scripts.Popup;
using GreiB.GameServices.Audio.Scripts;
using GreiB.UIManager.Scripts.Base;
using GreiB.UIManager.Scripts.UIPopup;
using UnityEngine;
using static PopupListProducts;

public class GameHud : MonoBehaviour
{
    [SerializeField] private BuyItem buyItem;
    [SerializeField] private GameObject joystick;
    [SerializeField] private Joystick joystickMove;
    [SerializeField] private Joystick joystickLook;

    private void Start()
    {
        this.buyItem.gameObject.SetActive(false);
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
}