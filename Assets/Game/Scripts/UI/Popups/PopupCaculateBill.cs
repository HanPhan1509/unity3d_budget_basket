using _GAME.Scripts;
using _GAME.Scripts.Controllers;
using DG.Tweening;
using GreiB.GameServices.Audio.Scripts;
using GreiB.GameServices.SaveData;
using GreiB.UIManager.Scripts.Base;
using GreiB.UIManager.Scripts.UIPopup;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopupCaculateBill : UIPopup
{
    [SerializeField] private GameObject _tabCart;
    [SerializeField] private GameObject _tabCal;

    [Header("CART")]
    [SerializeField] private BillItem _prefBillItem;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Text _txtPrice;
    [SerializeField] private Button btnCaculate;

    [SerializeField] private List<BillItem> _items = new();
    private LevelData LevelData;
    private float totalPrice = 0;

    protected override void OnShowing()
    {
        base.OnShowing();

        //param = (PopupLevelTargetParam)Parameter;
        gBot.SetActive(false);
        //slider.gameObject.SetActive(false);
        LevelData = GameManager.Instance.GetCurrentLevelData();
        ShowShoppingCart();
    }

    protected override void OnHidden()
    {
        base.OnHidden();

        _items.ForEach(item =>
        {
            item.transform.SetParent(null);
            SimplePool.Despawn(item.gameObject);
        });

        _items.Clear();
    }

    private void ShowShoppingCart()
    {
        _tabCart.SetActive(true);
        _tabCal.SetActive(false);

        _txtPrice.text = "$0.0";
        btnCaculate.interactable = false;

        _items.Clear();
        foreach (var product in GameController.Instance.LstProdutsInCart)
        {
            var pref = Instantiate(_prefBillItem.gameObject, Vector3.zero, Quaternion.identity, scrollRect.content);
            pref.gameObject.transform.localScale = Vector3.one;
            var item = pref.GetComponent<BillItem>();
            if (item != null)
            {
                item.Set(product);
                _items.Add(item);
            }
        }

        _ = CaculatePriceAsync();
    }

    private async Task CaculatePriceAsync()
    {
        await Task.Delay(2000);
        totalPrice = 0;
        foreach (var item in _items)
        {
            var price = item.GetPrice();
            totalPrice += price;
            await Task.Delay(1500);
            SetPrice();
            //await Task.Delay(1000);
            Destroy(item.gameObject);
        }
        btnCaculate.interactable = true;
        _items.Clear();
    }

    private void SetPrice()
    {
        //float rounded = Mathf.Round(totalPrice * 10f) / 10f;
        //_txtPrice.text = $"{rounded.ToString("0.0")}";
        AudioManager.Instance.PlaySfx(AudioName.GP_TAPTAP);
        _txtPrice.text = $"{totalPrice.ToString("0.0")}";
    }

    [Space(5.0f)]
    [Header("CALCULATOR")]
    [SerializeField] private GameObject gBot;
    [SerializeField] private GameObject btnNext;
    [SerializeField] private Text txtTotal;
    [SerializeField] private Text txtDiscountPercent;
    [SerializeField] private Text txtDiscount;
    [SerializeField] private Text txtVatPercent;
    [SerializeField] private Text txtVat;
    [SerializeField] private Text txtGrandTotal;

    //[Space(5.0f)]
    //[Header("SLIDER")]
    //[SerializeField] private Slider slider;

    private float grandTotal = 0;

    public void Summary()
    {
        _tabCart.SetActive(false);
        _tabCal.SetActive(true);
        ShowCaculatorTheBill();
    }

    private void ShowCaculatorTheBill()
    {
        _txtPrice.text = "CALCULATING...";

        txtTotal.text = $"{totalPrice.ToString("0.0")}";

        txtDiscountPercent.text = $"Discount ({LevelData.Sale}%)";
        float discount = totalPrice * (LevelData.Sale / 100f);
        txtDiscount.text = $"{discount.ToString("0.0")}";

        txtVatPercent.text = $"VAT ({LevelData.Vat}%)";
        float vatAmount = totalPrice * (LevelData.Vat / 100f);
        txtVat.text = $"{vatAmount.ToString("0.0")}";

        grandTotal = totalPrice - discount + vatAmount;
        //txtGrandTotal.text = $"{grandTotal}";
        txtGrandTotal.color = Color.white;
        float currentValue = 0f;
        DOTween.To(() => currentValue, x =>
        {
            currentValue = x;
            txtGrandTotal.text = currentValue.ToString("0.0"); // format 0.0
        }, grandTotal, 1.5f)
            .OnComplete(() =>
            {
                CheckEndGame();
            });
    }

    private void CheckEndGame()
    {
        float refund = LevelData.BudgetMoney - grandTotal;
        Debug.Log($"Refund {refund} = {LevelData.BudgetMoney} - {grandTotal}");
        GameController.Instance.GameHud.UpdateMoney(LevelData.BudgetMoney, (int)refund, 1.0f);
        gBot.SetActive(true);

        bool isWin = (refund == 0);
        btnNext.SetActive(isWin && !(LevelData.Level == GameManager.Instance.levelDatas[^1].Level));
        if (isWin)
        {
            txtGrandTotal.color = Color.green;
            AudioManager.Instance.PlaySfx(AudioName.UI_Transition_Door_Ting);
            _txtPrice.text = "VICTORY";
            if (LevelData.Level == SaveDataHandler.Instance.saveData.level)
            {
                SaveDataHandler.Instance.saveData.level += 1;
                //slider.gameObject.SetActive(true);
                //SetSlider(refund);
            }
        }
        else
        {
            txtGrandTotal.color = Color.red;
            _txtPrice.text = "FAILED";
        }
    }

    //private void SetSlider(float refund)
    //{
    //    float bonus = refund / 10;
    //    float progress = SaveDataHandler.Instance.saveData.progressVoucher;
    //    slider.value = progress;
    //    float temp = 0;
    //    if (progress + bonus < 1000)
    //    {
    //        progress += bonus;
    //        SaveDataHandler.Instance.saveData.progressVoucher += progress;
    //        slider.DOValue(progress, 0.1f).OnComplete(() =>
    //        {

    //        });
    //    }
    //    else
    //    {
    //        SaveDataHandler.Instance.saveData.voucherAmount += 1;
    //        temp = 1000 - (SaveDataHandler.Instance.saveData.progressVoucher + bonus);
    //        progress = 1000;
    //        SaveDataHandler.Instance.saveData.progressVoucher = temp;
    //        slider.DOValue(progress, 0.1f).OnComplete(() =>
    //        {
    //            slider.DOValue(temp, 0.1f).OnComplete(() =>
    //            {

    //            });
    //        });
    //    }
    //}

    public void Home()
    {
        Hide();
        UIManager.Instance.ShowTransition(() => { SceneManager.LoadScene(GameConstants.SceneMain); });
    }

    public void Replay()
    {
        Hide();
        UIManager.Instance.ShowTransition(() => { SceneManager.LoadScene(GameConstants.SceneGame); });
    }
    public void Next()
    {
        Hide();
        UIManager.Instance.ShowTransition(() =>
        {
            GameManager.Instance.SetCurrentLevelData(LevelData.Level + 1);
            SceneManager.LoadScene(GameConstants.SceneGame);
        });
    }
}