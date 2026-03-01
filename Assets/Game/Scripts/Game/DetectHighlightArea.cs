using _GAME.Scripts.Controllers;
using System;
using UnityEngine;

public class DetectHighlightArea : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] particleSystems;
    private Action<bool> OnShow = null;

    private void Start()
    {
        foreach (var particleSystem in particleSystems)
        {
            particleSystem.Play();
        }
    }

    public void Set(Action<bool> OnShow)
    {
        this.OnShow = OnShow;
    }    

    private void OnTriggerEnter(Collider other)
    {
        if (other)
        {
            string layername = LayerMask.LayerToName(other.gameObject.layer);
            Debug.Log($"Trigger enter layername {layername}");
            if (layername == "Player")
            {
                this.OnShow?.Invoke(true);
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
                this.OnShow?.Invoke(false);
            }
        }
    }
}
