using _GAME.Scripts.Views;
using GreiB.GameServices.SaveData;
using GreiB.UIManager.Scripts.Base;
using GreiB.UIManager.Scripts.UIPopup;
using GreiB.UIManager.Scripts.UIView;
using Imba.Utils;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace _GAME.Scripts.Controllers
{
    public class GameController : ManualSingletonMono<GameController>
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private GameHud gameHud;
        [SerializeField] private PlayerController playerController;

        private bool _isGamePaused = false;
        private int _userScore = 0;
        private GameView _gameView;

        public GameHud GameHud => gameHud;

        public override void Awake()
        {
            base.Awake();
        }

        void Start()
        {
            this._camera.SetActive(false);
            //_gameView = UIManager.Instance.ViewManager.GetViewByName<GameView>(UIViewName.GameView);
            //UIManager.Instance.ViewManager.ShowView(UIViewName.GameView);
            //UIManager.Instance.HideTransition(() => { });
        }

        void Update()
        {
            if (_isGamePaused)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                ShowEndGame();
            }
        }

        public void PauseGame()
        {
            _isGamePaused = true;
        }

        public void ResumeGame()
        {
            _isGamePaused = false;
        }

        /// <summary>
        /// GAME ENDED
        /// </summary>
        public void ShowEndGame()
        {
            PauseGame();
            
            EndGamePopupParam param = new EndGamePopupParam
            {
                score = _userScore,
                isNewHighScore = _userScore > SaveDataHandler.Instance.saveData.voucherAmount
            };

            //if (_userScore > SaveDataHandler.Instance.VoucherAmount)
            //{
            //    SaveDataHandler.Instance.VoucherAmount = _userScore;
            //    SaveDataHandler.Instance.RequestSave();
            //}

            UIManager.Instance.PopupManager.ShowPopup(UIPopupName.EndGamePopup, param);
        }

        public void CalculateTheBill()
        {
            this._camera.SetActive(true);
            this.playerController.StopMove();
            this.playerController.SetActive(false);
            this.gameHud.HideAll();
            UIManager.Instance.PopupManager.ShowPopup(UIPopupName.PopupCaculateBill);
        }

        private List<Product> _products = new List<Product>();
        //public bool IsInShoppingCart(Fruit productId)
        //{
        //    return GetProductInCart(productId) != null;
        //}

        //public Product GetProductInCart(string productId) {
        //    return _products.Find(x => x.Id == productId);
        //}

        //public bool AddProductInCart(Product product)
        //{
        //    if(product == null) 
        //        return false;

        //    //Full cart need upgrade
        //    if(_products.Count >= GameConfig.Instance.MaxItemInShoppingCart)
        //        return false;

        //    if(IsInShoppingCart(product.Id))
        //        return false;

        //    _products.Add(product);
        //    return true;
        //}

        //public bool RemoveProductInCart(string productId)
        //{
        //    if(string.IsNullOrEmpty(productId)) return false;
        //    if (IsInShoppingCart(productId))
        //    {
        //        var item = GetProductInCart(productId);
        //        _products.Remove(item);
        //        return true;
        //    }
        //    return false;
        //}

        private List<Product> _lstProdutsInCart = new List<Product>();

        public int GetQuantityByProductId(string productId)
        {
            Product pr = _lstProdutsInCart.Find(x => x.Id.ToString() == productId);
            return (pr == null) ? 0 : pr.Quantity;
        }
        public void UpdateShoppingCart(Product product)
        {
            Product pr = _lstProdutsInCart.Find(x => x.Id.ToString() == product.Id.ToString());
            if(pr == null)
            {
                _lstProdutsInCart.Add(product);
                return;
            }
            pr.Quantity = product.Quantity;
        }

        public void RemoveShoppingCart(Product product)
        {
            Product pr = _lstProdutsInCart.Find(x => x.Id.ToString() == product.Id.ToString());
            if (pr == null) return;
            if (pr.Quantity > 0)
            {
                pr.Quantity = product.Quantity;
            } else
            {
                _lstProdutsInCart.Remove(pr);
            }
        }
    }
}