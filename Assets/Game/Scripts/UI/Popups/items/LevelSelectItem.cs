using GreiB.GameServices.SaveData;
using System;
using UnityEngine;
using UnityEngine.UI;


public class LevelSelectItem : MonoBehaviour
{
    [SerializeField] private GameObject glock;
    [SerializeField] private Button btnLevel;
    [SerializeField] private GameObject[] star = new GameObject[3];
    [SerializeField] private Sprite[] bg = new Sprite[2];
    [SerializeField] private Image _img;
    [SerializeField] private Text _text;
    private int _index;
    private Action<int> OnClicked;
    public void Set(int level, int starCount, Action<int> OnClicked)
    {
        this._index = level;
        this.OnClicked = OnClicked;
        bool isLock = level > SaveDataHandler.Instance.saveData.level;
        glock.SetActive(isLock);
        _text.SetActive(!isLock);
        btnLevel.interactable = !isLock;
        for (int i = 0; i < star.Length; i++)
        {
            this.star[i].SetActive(i < starCount);
            _text.text = $"{level + 1}";
            _img.sprite = bg[(level + 1) % 5 == 0 ? 1 : 0];
        }
    }

    public void OnClickedLevelSelect()
    {
        this.OnClicked?.Invoke(this._index);
    }
}
