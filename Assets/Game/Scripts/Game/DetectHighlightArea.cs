using _GAME.Scripts.Controllers;
using UnityEngine;

public class DetectHighlightArea : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] particleSystems;
    private Stall _stall = null;

    private void Start()
    {
        foreach (var particleSystem in particleSystems)
        {
            particleSystem.Play();
        }
    }

    public void Set(Stall stall)
    {
        this._stall = stall;
    }    

    private void OnTriggerEnter(Collider other)
    {
        if (other)
        {
            if (_stall != null)
            {
                string layername = LayerMask.LayerToName(other.gameObject.layer);
                Debug.Log($"Trigger enter layername {layername}");
                if (layername == "Player")
                {
                    GameController.Instance.GameHud.ShowBuyItem(this._stall);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other)
        {
            string layername = LayerMask.LayerToName(other.gameObject.layer);
            Debug.Log($"Trigger exit layername {layername}");
            if (layername == "Player")
            {
                GameController.Instance.GameHud.ShowBuyItem();
            }
        }
    }
}
