using UnityEngine;

public class StallProduct : MonoBehaviour
{
    [SerializeField] private Stall stall;
    [SerializeField] private DetectHighlightArea detectHighlightArea;

    private void Start()
    {
        this.detectHighlightArea.Set(stall);
    }

}
