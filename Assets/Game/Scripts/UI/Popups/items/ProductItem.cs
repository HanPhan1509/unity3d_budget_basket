using _GAME.Scripts.Controllers;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ProductItem : MonoBehaviour
{
    [SerializeField] private Image imgPreview;
    [SerializeField] private Text txtName;
    [SerializeField] private Text txtPrice;
    [SerializeField] private Text txtQuantity;
    [SerializeField] private GameObject _btnGet;
    [SerializeField] private GameObject _btnReturn;
    [SerializeField] private GameObject _gQuantity;
    private Product product;
    private Action<ProductItem> OnGet;
    private Action<ProductItem> OnReturn;

    public Product Product { get => product; set => product = value; }

    public void Set(Product product, Action<ProductItem> OnGet, Action<ProductItem> OnReturn)
    {
        this.product = product;
        this.OnGet = OnGet;
        this.OnReturn = OnReturn;

        UpdateView();
    }

    public void UpdateView()
    {
        if (product != null)
        {
            //imgPreview.sprite = product.Preview;
            txtName.text = product.Id.ToString().Replace("_", " ");
            txtPrice.text = $"Price: {product.Price}";
            txtQuantity.text = $"{GameController.Instance.GetQuantityByProductId(product.Id.ToString())}/{product.MaxQuantity}";

            //bool isInShoppingCart = GameController.Instance.IsInShoppingCart(product.Id);
            //_btnGet.SetActive(!isInShoppingCart);
            //_btnReturn.SetActive(isInShoppingCart);

            imgPreview.sprite = Resources.Load<Sprite>($"stalls/{product.Id.ToString()}");
        }
    }

    public void OnClickedGetTheItem()
    {
        if (product == null) return;
        OnGet?.Invoke(this);
    }

    public void OnClickedReturnTheItem()
    {
        if (product == null) return;
        OnReturn?.Invoke(this);
    }

    public void BTN_Add()
    {
        if (product == null) return;
        this.product.Quantity += 1;
        this.product.Quantity = Mathf.Clamp(this.product.Quantity, 0, this.product.MaxQuantity);
        txtQuantity.text = $"{product.Quantity}/{product.MaxQuantity}";
        Debug.Log($"Add: {product.Id.ToString()} - {product.Quantity}/{product.MaxQuantity}");
        GameController.Instance.UpdateShoppingCart(this.product);
    }

    public void BTN_Minus()
    {
        if(product == null) return;
        this.product.Quantity -= 1;
        this.product.Quantity = Mathf.Clamp(this.product.Quantity, 0, this.product.MaxQuantity);
        txtQuantity.text = $"{product.Quantity}/{product.MaxQuantity}";
        Debug.Log($"Minus: {product.Id.ToString()} - {product.Quantity}/{product.MaxQuantity}");
        GameController.Instance.RemoveShoppingCart(this.product);
    }
}
