using _GAME.Scripts.Controllers;
using GreiB.UIManager.Scripts.UIPopup;
using Unity.VisualScripting;
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
    [SerializeField] private GameObject prefab;

    protected override void OnShowing()
    {
        base.OnShowing();

        var param = (PopupListProductsParam)Parameter;
        if (param != null)
        {
            foreach (var pro in param.stall.Products)
            {
                var go = SimplePool.Spawn(prefab, Vector3.zero, Quaternion.identity);
                go.transform.SetParent(scrollView.content);
                go.transform.localScale = Vector3.one;
                var productItem = go.GetComponent<ProductItem>();
                if (productItem != null)
                {
                    Debug.Log($"Set {pro?.Name} - {pro?.Price}");
                    productItem.Set(pro, Get, Return);
                }
            }
        }
    }

    protected override void OnHidden()
    {
        base.OnHidden();
        foreach (Transform child in scrollView.content)
        {
            SimplePool.Despawn(child.gameObject);
        }
    }

    private void Get(ProductItem productItem)
    {
        if (GameController.Instance.AddProductInCart(productItem.Product))
            productItem.UpdateView();
    }

    private void Return(ProductItem productItem)
    {
        if (GameController.Instance.RemoveProductInCart(productItem.Product.id))
            productItem.UpdateView();
    }
}
