using _GAME.Scripts.Controllers;
using GreiB.UIManager.Scripts.UIPopup;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class PopupListProducts : UIPopup
{
    public class PopupListProductsParam
    {
        public Stall stall;
    }

    [SerializeField] private Text txtNameStall;
    [SerializeField] private ScrollRect scrollView;
    [SerializeField] private Transform parent;
    [SerializeField] private Text txtSale;
    [SerializeField] private GameObject sale;
    [SerializeField] private ProductItem prefab;
    private List<ProductItem> items = new();

    protected override void OnShowing()
    {
        base.OnShowing();

        var param = (PopupListProductsParam)Parameter;
        if (param != null)
        {
            var data = GameManager.Instance.GetCurrentLevelData();
            var saleProduct = data.IsSaleForStall(param.stall.StallID);
            sale.SetActive(saleProduct != null);
            if (saleProduct != null)
            {
                txtSale.text = $"{saleProduct.sale}%";
            }

            //txtNameStall.text = param.stall.StallID.ToString().Replace("_", " ");
            Debug.Log($"================{param.stall.StallID} - {param.stall.Products.Count}");
            for (int i = 0; i < param.stall.Products.Count; i++)
            {
                Product pr = param.stall.Products[i];
                var productItem = GetItem(i);
                if (productItem != null)
                {
                    productItem.Set(pr, Get, Return);
                    productItem.SetActive(true);
                }
            }
        }
        scrollView.verticalNormalizedPosition = 1;
    }

    private ProductItem GetItem(int index)
    {
        if (index >= items.Count)
        {
            var go = Instantiate(prefab.gameObject, Vector3.zero, Quaternion.identity, parent);
            go.transform.localScale = Vector3.one;
            var productItem = go.GetComponent<ProductItem>();
            items.Add(productItem);
            return productItem;
        }
        else
        {
            return items[index];
        }
    }

    protected override void OnHidden()
    {
        base.OnHidden();
        items.ForEach(item => item.SetActive(false));
        GameController.Instance.PlayerController.AllowMove = true;
    }

    private void Get(ProductItem productItem)
    {
        //if (GameController.Instance.AddProductInCart(productItem.Product))
        //    productItem.UpdateView();
    }

    private void Return(ProductItem productItem)
    {
        //if (GameController.Instance.RemoveProductInCart(productItem.Product.Id))
        //    productItem.UpdateView();
    }
}
