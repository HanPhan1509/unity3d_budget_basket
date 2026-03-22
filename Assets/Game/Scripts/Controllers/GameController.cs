using _GAME.Scripts.Views;
using GreiB.GameServices.SaveData;
using GreiB.UIManager.Scripts.Base;
using GreiB.UIManager.Scripts.UIPopup;
using GreiB.UIManager.Scripts.UIView;
using Imba.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static PopupLevelTarget;
using static PopupListProducts;

namespace _GAME.Scripts.Controllers
{
    public class GameController : ManualSingletonMono<GameController>
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private GameHud gameHud;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private StallProduct _cashRegister;

        private bool _isGamePaused = false;
        private bool _isGameEnded = false;
        private GameView _gameView;

        public GameHud GameHud => gameHud;
        private LevelData _levelData;

        public List<Product> LstProdutsInCart { get => _lstProdutsInCart; set => _lstProdutsInCart = value; }
        public PlayerController PlayerController { get => playerController; set => playerController = value; }

        public override void Awake()
        {
            base.Awake();
        }

        void Start()
        {
            this._camera.SetActive(false);
            _levelData = GameManager.Instance.GetCurrentLevelData();
            gameHud.Set();

            UIManager.Instance.PopupManager.ShowPopup(UIPopupName.PopupLevelTarget, new PopupLevelTargetParam
            {
                OnPlay = GameStart
            });

            UIManager.Instance.HideTransition(() =>
            {

            });


            //UIManager.Instance.PopupManager.ShowPopup(UIPopupName.PopupListProducts, new PopupListProductsParam { stall = stall });
            //_gameView = UIManager.Instance.ViewManager.GetViewByName<GameView>(UIViewName.GameView);
            //UIManager.Instance.ViewManager.ShowView(UIViewName.GameView);
            //UIManager.Instance.HideTransition(() => { });
        }

        private void GameStart()
        {
            GameManager.Instance.IsPlaying = true;
            _isGameEnded = false;
            _isGamePaused = false;
            StartCoroutine(Countdown());
        }

        private IEnumerator Countdown()
        {
            float timeLeft = _levelData.Timer;
            while (timeLeft >= 0)
            {
                gameHud.UpdateTimer(timeLeft);
                if (timeLeft <= 0)
                {
                    ShowEndGame();
                    yield break;
                }
                if (_isGameEnded)
                {
                    break;
                }
                yield return new WaitForSeconds(1f);
                if (_isGamePaused) continue;
                timeLeft--;
            }
        }

        void Update()
        {
            if (_isGamePaused)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                CalculateTheBill();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                gameHud.ButtonSettings();
            }
        }

        public void PauseGame()
        {
            _isGamePaused = true;
            playerController.AllowMove = false;
        }

        public void ResumeGame()
        {
            _isGamePaused = false;
            playerController.AllowMove = true;
        }

        /// <summary>
        /// GAME ENDED
        /// </summary>
        public void ShowEndGame()
        {
            _isGameEnded = true;
            UIManager.Instance.ShowTransition(() =>
            {
                this._camera.SetActive(true);
                this.playerController.StopMove();
                this.playerController.SetActive(false);
                this.gameHud.HideAll();
                UIManager.Instance.PopupManager.HidePopup(UIPopupName.SettingPopup); //if any
                UIManager.Instance.PopupManager.HidePopup(UIPopupName.PopupListProducts); //if any
                UIManager.Instance.HideTransition(() =>
                {
                    UIManager.Instance.PopupManager.ShowPopup(UIPopupName.EndGamePopup);
                });
            });
        }

        public void CalculateTheBill()
        {
            _isGameEnded = true;
            UIManager.Instance.ShowTransition(() =>
            {
                this._camera.SetActive(true);
                this.playerController.StopMove();
                this.playerController.SetActive(false);
                this.gameHud.HideAll();
                UIManager.Instance.PopupManager.HidePopup(UIPopupName.SettingPopup); //if any
                UIManager.Instance.PopupManager.HidePopup(UIPopupName.PopupListProducts); //if any
                UIManager.Instance.HideTransition(() =>
                {
                    UIManager.Instance.PopupManager.ShowPopup(UIPopupName.PopupCaculateBill);
                });
            });
        }

        private List<Product> _lstProdutsInCart = new List<Product>();
        private Stall _currentStall = null;

        public void SetCurrentStall(Stall stall)
        {
            _currentStall = stall;
            this.gameHud.ShowBuyItem(stall);
        }

        public int GetQuantityByProductId(string productId)
        {
            Product pr = _lstProdutsInCart.Find(x => x.Id.ToString() == productId);
            return (pr == null) ? 0 : pr.Quantity;
        }
        public void UpdateShoppingCart(Product product)
        {
            Product pr = _lstProdutsInCart.Find(x => x.Id.ToString() == product.Id.ToString());

            if (pr == null)
            {
                _lstProdutsInCart.Add(product);
                this.gameHud.UpdateTarget(product.Id, product.Quantity);
                UpdateTarget();
                CheckUnlockCashRegister();
                return;
            }
            pr.Quantity = product.Quantity;
            this.gameHud.UpdateTarget(pr.Id, pr.Quantity);
            UpdateTarget();
            CheckUnlockCashRegister();
        }

        public void RemoveShoppingCart(Product product)
        {
            Product pr = _lstProdutsInCart.Find(x => x.Id.ToString() == product.Id.ToString());
            if (pr == null) return;
            if (pr.Quantity > 0)
            {
                pr.Quantity = product.Quantity;
            }
            else
            {
                _lstProdutsInCart.Remove(pr);
            }
            this.gameHud.UpdateTarget(product.Id, pr.Quantity);
            UpdateTarget();
            CheckUnlockCashRegister();
        }

        private void UpdateTarget()
        {
            if (_currentStall != null)
            {
                int quantity = 0;
                //lay san pham thuoc stall hien tai
                var cartProductsInStall = _lstProdutsInCart.FindAll(x => _currentStall.IsInStall(x.Id));
                cartProductsInStall.ForEach(x => quantity += x.Quantity);

                this.gameHud.UpdateTarget(_currentStall.StallID, quantity);
            }
        }

        private void CheckUnlockCashRegister()
        {
            bool isAllCompleted = gameHud.IsCompletedAllTarget();
            Debug.Log($"CheckUnlockCashRegister = {isAllCompleted}");
            _cashRegister.OnZone(isAllCompleted);
        }
    }
}