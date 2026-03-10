using GreiB.GameServices.SaveData;
using GreiB.UIManager.Scripts.UIPopup;
using System;
using UnityEngine;
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
    }
}