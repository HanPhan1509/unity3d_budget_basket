using _GAME.Scripts.Popup;
using GreiB.GameServices.Audio.Scripts;
using GreiB.GameServices.SaveData;
using GreiB.UIManager.Scripts.Base;
using GreiB.UIManager.Scripts.UIPopup;
using GreiB.UIManager.Scripts.UIView;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _GAME.Scripts.Views
{
    public class MainView : UIView
    {
        [SerializeField] private Text txtVoucher;
        [SerializeField] private Text txtPoint;

        protected override void OnShowing()
        {
            base.OnShowing();
            txtVoucher.text = SaveDataHandler.Instance.saveData.voucherAmount.ToString();
            txtPoint.text = SaveDataHandler.Instance.saveData.point.ToString();
        }


        #region MAIN UI BUTTON CALLBACK

        public void OnClickPlayGame()
        {
            UIManager.Instance.PopupManager.ShowPopup(UIPopupName.PopupLevelSelect);
        }

        public void OnClickOpenSetting()
        {
            var param = new SettingPopupParam
            {
                showGroupBtn = false
            };
            AudioManager.Instance.PlaySfx(AudioName.UI_Click);
            UIManager.Instance.PopupManager.ShowPopup(UIPopupName.SettingPopup, param);
        }


        public void OnClickLeaderBoard()
        {
            AudioManager.Instance.PlaySfx(AudioName.UI_Click);
            UIManager.Instance.PopupManager.ShowPopup(UIPopupName.LeaderBoardPopup);
        }
        public void OnClickHowToPlay()
        {
            AudioManager.Instance.PlaySfx(AudioName.UI_Click);
            UIManager.Instance.PopupManager.ShowPopup(UIPopupName.TutorialPopup);
        }
        #endregion
    }
}