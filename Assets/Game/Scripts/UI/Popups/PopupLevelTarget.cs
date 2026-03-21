using GreiB.UIManager.Scripts.UIPopup;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;


public class PopupLevelTarget : UIPopup
{
    public class PopupLevelTargetParam
    {
        public Action OnPlay;
    }

    [SerializeField] private GameObject prefTarget;
    [SerializeField] private Text txtLevel;
    [SerializeField] private Text txtTime;
    [SerializeField] private Text txtVat;
    [SerializeField] private RectTransform parent;
    [SerializeField] private GameObject _sale;
    [SerializeField] private GameObject _vat;
    [SerializeField] private GameObject _time;
    private PopupLevelTargetParam param;
    private LevelData LevelData;
    private List<TargetItem> _items = new();

    protected override void OnShowing()
    {
        base.OnShowing();

        _items.Reverse();
        _items.ForEach(item => SimplePool.Despawn(item.gameObject));

        param = (PopupLevelTargetParam)Parameter;
        LevelData = GameManager.Instance.GetCurrentLevelData();
        if (param != null)
        {
            txtLevel.text = $"Level {LevelData.Level + 1}";

            TimeSpan ts = TimeSpan.FromSeconds(LevelData.Timer);
            string formatted = string.Format("{0:D2}:{1:D2}", ts.Minutes, ts.Seconds);
            this.txtTime.text = $"{formatted}";

            txtVat.text = $"All products in the store are subject \r\nto a {LevelData.Vat}% VAT on the total bill.";

            _time.SetActive(LevelData.Timer > 0);
            _sale.SetActive(LevelData.Sale > 0);
            _vat.SetActive(LevelData.Vat > 0);

            foreach (var tg in LevelData.TargetStalls)
            {
                var pref = SimplePool.Spawn(prefTarget, Vector3.zero, Quaternion.identity);
                var item = pref.GetComponent<TargetItem>();
                if (item != null)
                {
                    item.transform.SetParent(parent);
                    item.gameObject.transform.localScale = Vector3.one;
                    item.Set(tg);
                    _items.Add(item);
                }
            }

            foreach (var tg in LevelData.TargetProducts)
            {
                var pref = SimplePool.Spawn(prefTarget, Vector3.zero, Quaternion.identity);
                var item = pref.GetComponent<TargetItem>();
                if (item != null)
                {
                    item.transform.SetParent(parent);
                    item.gameObject.transform.localScale = Vector3.one;
                    item.Set(tg);
                    _items.Add(item);
                }
            }
        }
    }

    public void OnClicked()
    {
        this.Hide();
        this.param.OnPlay?.Invoke();
    }
}
