using _GAME.Scripts.Popup;
using GreiB.GameServices.Audio.Scripts;
using GreiB.UIManager.Scripts.Base;
using GreiB.UIManager.Scripts.UIPopup;
using UnityEngine;

public class GameHud : MonoBehaviour
{
    [SerializeField] private BuyItem buyItem;

    private void Start()
    {
        this.buyItem.gameObject.SetActive(false);
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
        //open UI
        UIManager.Instance.PopupManager.ShowPopup(UIPopupName.PopupListProducts);
    }
}