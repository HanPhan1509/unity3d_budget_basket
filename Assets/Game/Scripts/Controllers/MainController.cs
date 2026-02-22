using GreiB.UIManager.Scripts.Base;
using GreiB.UIManager.Scripts.UIView;
using UnityEngine;

namespace _GAME.Scripts.Controllers
{
    public class MainController : MonoBehaviour
    {
        private void Awake()
        {
            UIManager.Instance.ViewManager.ShowView(UIViewName.MainView);
            UIManager.Instance.HideTransition(() => {  });
        }
    }
}