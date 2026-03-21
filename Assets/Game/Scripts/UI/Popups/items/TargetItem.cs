using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetItem : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Text txtAmount;
    public void Set(TargetStall target)
    {
        if (image != null)
        {

            string path = $"stalls/{target.stallID}";
            var sprite = Resources.Load<Sprite>(path);
            image.sprite = sprite;
        }

        this.txtAmount.text = $"x{target.amount}";
    }

    public void Set(TargetProduct target)
    {
        if (image != null)
        {

            string path = $"products/{target.productID}";
            var sprite = Resources.Load<Sprite>(path);
            image.sprite = sprite;
        }

        this.txtAmount.text = $"x{target.amount}";
    }
}