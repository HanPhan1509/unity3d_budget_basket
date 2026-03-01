using _GAME.Scripts.Controllers;
using UnityEngine;

public class StallProduct : MonoBehaviour
{
    [SerializeField] private Stall stall;
    [SerializeField] private DetectHighlightArea detectHighlightArea;

    private void Start()
    {
        this.detectHighlightArea.Set(ShowList);
    }

    private void ShowList(bool isShow)
    {
        GameController.Instance.GameHud.ShowBuyItem(isShow ? this.stall : null);
    }
}