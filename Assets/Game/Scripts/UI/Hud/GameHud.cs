using UnityEngine;

public class GameHud : MonoBehaviour
{
    [SerializeField] private BuyItem buyItem;

    private void Start()
    {
        this.buyItem.gameObject.SetActive(false);
    }

    public void ShowBuyItem(Stall stall = null)
    {
        buyItem.SetActive(stall != null);
        if (stall != null)
        {
            buyItem.Set(stall, OnOpenProductListInStall);
        } else
        {
            buyItem.Reset();
        }
    }

    private void OnOpenProductListInStall(Stall stall)
    {
        //open UI
    }
}