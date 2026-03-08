using _GAME.Scripts.Controllers;
using UnityEngine;
using UnityEngine.UI;

public class BillItem : MonoBehaviour
{
    [SerializeField] private Image imgPreview;
    [SerializeField] private Text txtName;
    [SerializeField] private Text txtPrice;
    [SerializeField] private Text txtTotal;
    private Product product;
    public void Set(Product product)
    {
        this.product = product;
        this.UpdateView();
    }

    public void UpdateView()
    {
        if (product != null)
        {
            //imgPreview.sprite = product.Preview;
            txtName.text = product.Id.ToString();
            txtPrice.text = $"Price: {product.Price}";

            //bool isInShoppingCart = GameController.Instance.IsInShoppingCart(product.Id);
        }
    }
}
