using _GAME.Scripts;
using GreiB.UIManager.Scripts.Base;
using GreiB.UIManager.Scripts.UIPopup;
using UnityEngine.SceneManagement;

public class EndGamePopup : UIPopup
{
    public void OnClickExit()
    {
        Hide();
        UIManager.Instance.ShowTransition(() =>
        {
            SceneManager.LoadScene(GameConstants.SceneMain);
        });
    }

    public void OnClickPlayAgain()
    {
        Hide();
        UIManager.Instance.ShowTransition(() =>
        {
            SceneManager.LoadScene(GameConstants.SceneGame);
        });
    }
}