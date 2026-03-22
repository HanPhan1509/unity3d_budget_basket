using Imba.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : ManualSingletonMono<GameManager>
{
    public List<LevelData> levelDatas = new();
    private LevelData _currentLevelData;

    public void SetCurrentLevelData(int index)
    {
        int level = Mathf.Clamp(index, 0, levelDatas.Count - 1);
        this._currentLevelData = levelDatas[index];
    }

    public LevelData GetCurrentLevelData()
    {
        return _currentLevelData;
    }

    public bool IsLastLevel()
    {
        return _currentLevelData.Level == levelDatas[^1].Level;
    }
}
