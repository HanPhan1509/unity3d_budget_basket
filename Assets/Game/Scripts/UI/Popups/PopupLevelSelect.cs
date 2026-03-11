using _GAME.Scripts;
using _GAME.Scripts.Views;
using GreiB.GameServices.SaveData;
using GreiB.UIManager.Scripts.Base;
using GreiB.UIManager.Scripts.UIPopup;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopupLevelSelect : UIPopup
{
    [SerializeField] private GameObject _prefItem;
    [SerializeField] private ScrollRect _scrollRect;

    protected override void OnShowing()
    {
        base.OnShowing();

        var list = GameManager.Instance.levelDatas;
        for (int i = 0; i < list.Count; i++)
        {
            var pref = SimplePool.Spawn(_prefItem, Vector3.zero, Quaternion.identity);
            pref.transform.SetParent(this._scrollRect.content);
            var item = pref.GetComponent<LevelSelectItem>();
            if (item)
            {
                item.Set(i, SaveDataHandler.Instance.GetLevelSelect(i), OnClickedLevel);
            }
        }
    }

    private void OnClickedLevel(int level)
    {
        GameManager.Instance.SetCurrentLevelData(level);
        this.Hide();
        UIManager.Instance.ViewManager.GetViewByName<MainView>(GreiB.UIManager.Scripts.UIView.UIViewName.MainView).Hide();
        SceneManager.LoadScene(GameConstants.SceneGame);
        UIManager.Instance.ShowLoading(3f);
    }
}