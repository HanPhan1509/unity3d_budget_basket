using _GAME.Scripts;
using _GAME.Scripts.Views;
using GreiB.GameServices.SaveData;
using GreiB.UIManager.Scripts.Base;
using GreiB.UIManager.Scripts.UIPopup;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PopupLevelSelect : UIPopup
{
    [SerializeField] private GameObject _prefItem;
    [SerializeField] private ScrollRect _scrollRect;
    private List<LevelSelectItem> _items = new();
    protected override void OnShowing()
    {
        base.OnShowing();
        
        var list = GameManager.Instance.levelDatas;
        for (int i = 0; i < list.Count; i++)
        {
            var pref = SimplePool.Spawn(_prefItem, Vector3.zero, Quaternion.identity);
            pref.transform.SetParent(this._scrollRect.content);
            pref.transform.localScale = Vector3.one;
            var item = pref.GetComponent<LevelSelectItem>();
            if (item)
            {
                item.Set(i, SaveDataHandler.Instance.GetLevelSelect(i), OnClickedLevel);
                _items.Add(item);
            }
        }
    }

    protected override void OnHidden()
    {
        base.OnHidden();
        foreach (var item in _items)
        {
            item.transform.SetParent(null);
            SimplePool.Despawn(item.gameObject);
        }
        _items.Clear();
    }

    private void OnClickedLevel(int level)
    {
        GameManager.Instance.SetCurrentLevelData(level);
        this.Hide();
        UIManager.Instance.ViewManager.GetViewByName<MainView>(GreiB.UIManager.Scripts.UIView.UIViewName.MainView).Hide();
        UIManager.Instance.ShowTransition(() =>
        {
            SceneManager.LoadScene(GameConstants.SceneGame);
        });
        //UIManager.Instance.ShowLoading(3f);
    }
}