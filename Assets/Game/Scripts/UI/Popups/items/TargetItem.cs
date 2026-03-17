using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetItem : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Text txtAmount;
    private TargetLevel target;
    public void Set(TargetLevel target)
    {
        if (image != null)
        {

            string path = $"stalls/{target.stallID}";
            var sprite = Resources.Load<Sprite>(path);
            image.sprite = sprite;
        }

        this.txtAmount.text = $"x{target.amount}";
    }
}