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
        this._currentLevelData = levelDatas[index];
    }

    public LevelData GetCurrentLevelData()
    {
        return _currentLevelData;
    }
}
