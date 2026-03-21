using _GAME.Scripts.Controllers;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class BillItem : MonoBehaviour
{
    [SerializeField] private Image imgPreview;
    [SerializeField] private Text txtName;
    [SerializeField] private Text txtPrice;
    [SerializeField] private Text txtTotal;
    private Product product;
    //private Action<float> OnAddPrice;
    private float finalPrice = 0.0f;
    public void Set(Product product, Action<float> OnAddPrice = null)
    {
        this.product = product;
        //this.OnAddPrice = OnAddPrice;
        UpdateViewAsync();
    }

    public void UpdateViewAsync()
    {
        if (product != null)
        {
            string path = $"products/{product.Id}";
            var sprite = Resources.Load<Sprite>(path);
            imgPreview.sprite = sprite;
            txtName.text = product.Id.ToString().Replace("_", " ");
            txtPrice.text = $"Price: {product.Price} x{product.Quantity}";

            var total = product.Quantity * product.Price;
            float discount = total * (GetDiscountPercent() / 100f);
            finalPrice = total - discount;
            txtTotal.text = $"${Mathf.Max(finalPrice, 0f)}";

            //bool isInShoppingCart = GameController.Instance.IsInShoppingCart(product.Id);
            //await Task.Delay(1000);
            //this.OnAddPrice?.Invoke(finalPrice);
        }
    }

    private float GetDiscountPercent()
    {
        var data = GameManager.Instance.GetCurrentLevelData();
        //var pr = data.SaleProducts.Find(x => x.productID == this.product.Id); //TODO
        SaleProduct pr = null;
        if (pr == null) return 0.0f;
        return pr.sale;
    }

    public float GetPrice()
    {
        return finalPrice;
    }
}
