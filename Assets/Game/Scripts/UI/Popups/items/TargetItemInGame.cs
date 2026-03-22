using UnityEngine;
using UnityEngine.UI;

public class TargetItemInGame : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Text txtAmount;
    private TargetStall targetStall;
    private TargetProduct targetProduct;
    private int target = 0;

    public TargetStall TargetStall { get => targetStall; set => targetStall = value; }
    public TargetProduct TargetProduct { get => targetProduct; set => targetProduct = value; }

    public void Set(TargetStall target)
    {
        if (image != null)
        {

            string path = $"stalls/{target.stallID}";
            var sprite = Resources.Load<Sprite>(path);
            image.sprite = sprite;
        }
        this.target = target.amount;
        this.txtAmount.text = $"0/{target.amount}";
    }

    public void Set(TargetProduct target)
    {
        if (image != null)
        {
            string path = $"products/{target.productID}";
            var sprite = Resources.Load<Sprite>(path);
            image.sprite = sprite;
        }
        this.target = target.amount;
        this.txtAmount.text = $"0/{target.amount}";
    }

    public void UpdateQuantityTarget(int quantity)
    {
        var number = Mathf.Clamp(quantity, 0, this.target);
        this.txtAmount.text = $"{number}/{this.target}";
    }
}
