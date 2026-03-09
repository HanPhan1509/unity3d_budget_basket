using Imba.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : ManualSingletonMono<GameManager>
{
    public List<LevelData> levelData = new();
    private LevelData _currentLevelData;

    public LevelData GetCurrentLevelData()
    {
        return _currentLevelData;
    }
}
