using _GAME.Scripts.Controllers;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ProductItem : MonoBehaviour
{
    [SerializeField] private Image imgPreview;
    [SerializeField] private Text txtName;
    [SerializeField] private Text txtPrice;
    [SerializeField] private GameObject _btnGet;
    [SerializeField] private GameObject _btnReturn;
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
            imgPreview.sprite = product.Preview;
            txtName.text = product.Name;
            txtPrice.text = $"Price: {product.Price}";

            bool isInShoppingCart = GameController.Instance.IsInShoppingCart(product.id);
            _btnGet.SetActive(!isInShoppingCart);
            _btnReturn.SetActive(isInShoppingCart);
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
}
