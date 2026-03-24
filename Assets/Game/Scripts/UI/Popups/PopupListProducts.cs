using _GAME.Scripts.Controllers;
using GreiB.UIManager.Scripts.UIPopup;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private GameObject prefab;
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
            foreach (var pro in param.stall.Products)
            {
                var go = SimplePool.Spawn(prefab, Vector3.zero, Quaternion.identity);
                go.transform.SetParent(parent);
                go.transform.localScale = Vector3.one;
                var productItem = go.GetComponent<ProductItem>();
                if (productItem != null)
                {
                    Debug.Log($"{pro.Id}");
                    productItem.Set(pro, Get, Return);
                    items.Add(productItem);
                }
            }
        }
        scrollView.verticalNormalizedPosition = 1;
    }

    protected override void OnHidden()
    {
        base.OnHidden();
        GameController.Instance.PlayerController.AllowMove = true;
        foreach (var child in items)
        {
            child.gameObject.transform.SetParent(null);
            SimplePool.Despawn(child.gameObject);
        }
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
