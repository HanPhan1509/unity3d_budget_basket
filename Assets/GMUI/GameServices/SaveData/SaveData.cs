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

        public int level = 0;
        public int currentLevelIndex = 1;
        public int currentDayDailyReward = 1;
        public int point = 0;
        public int voucherAmount = 0;
        public int gameModeChoose = 0;
        public float progressVoucher = 0;

        public bool isShowRateUs;

        public List<int> dailyRewardClaimedList = new();
        public List<int> stars = new();
        
    }
}