using GreiB.UIManager.Scripts.UIPopup;
using System;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPopup : UIPopup
{
    [SerializeField] private Text txt;
    [SerializeField] private GameObject[] _tutors = new GameObject[4];
    private int index = 0;

    protected override void OnShowing()
    {
        base.OnShowing();
        txt.text = "Next";
        index = 0;
        Show();
    }

    public void Next()
    {
        index++;
        if (index >= _tutors.Length)
        {
            this.Hide();
            return;
        }
        if (index == _tutors.Length - 1)
        {
            txt.text = "Close";
        }
        Show();
    }

    private void Show()
    {
        for (int i = 0; i < _tutors.Length; i++)
        {
            _tutors[i].SetActive(i == index);
        }
    }
}
