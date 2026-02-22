using System;
using System.Collections.Generic;

namespace GreiB.GameServices.SaveData
{
    [Serializable]
    public class SaveData
    {
        public string profileName = "";
        public int playerRegion;
        public string userID = "";
        public string lastTimeEarnedDailyReward = DateTime.Now.ToString();
        public string lastDateMonthlyChallenge = DateTime.Now.ToString();
        public string rankingCountdownTime;

        public bool tapPlayTutorial;

        public int level = 1;
        public int currentLevelIndex = 1;
        public int moneyAmount;
        public int currentDayDailyReward = 1;
        public int userHighScore = 0;
        public int gameModeChoose = 0;
        
        public int userHighScoreTime = -1;

        public bool isShowRateUs;

        public List<int> dailyRewardClaimedList = new();
        
    }
}