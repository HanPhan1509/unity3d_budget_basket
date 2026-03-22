using _GAME.Scripts.Controllers;
using NaughtyAttributes;
using UnityEngine;
using static UnityEditor.Progress;

public class StallProduct : MonoBehaviour
{
    [SerializeField] private Stall stall;
    [SerializeField] private DetectHighlightArea detectHighlightArea;

    private void Start()
    {
        this.detectHighlightArea.Set(ShowList);
        if (this.stall.StallID != StallID.CashRegister)
            OnZone(true);
        else
            OnZone(false);
    }

    private void ShowList(bool isShow)
    {
        GameController.Instance.SetCurrentStall(isShow ? this.stall : null);
    }

    public void OnZone(bool isOn)
    {
        if (isOn)
        {
            detectHighlightArea.SetActive(true);
            detectHighlightArea.On();
        }
        else
        {
            detectHighlightArea.SetActive(false);
            detectHighlightArea.Off();
        }
    }


    //test
    [Header("DATA")]
    public int start = 0;
    public int end = 0;
    public int maxQuantity = 1;
    [Button("Load product id")]
    public void Load()
    {
        this.stall.Products.Clear();
        for (int i = start; i <= end; i++)
        {
            var item = new Product();
            item.Id = (ProductID)i;
            item.MaxQuantity = maxQuantity;
            this.stall.Products.Add(item);
        }
    }

    [Button("RELOAD")]
    public void Reload()
    {
        int index = start;
        for (int i = 0; i < this.stall.Products.Count; i++)
        {
            //this.stall.Products[i].Id = (ProductID)index;
            this.stall.Products[i].MaxQuantity = maxQuantity;
            index++;
        }
    }
}