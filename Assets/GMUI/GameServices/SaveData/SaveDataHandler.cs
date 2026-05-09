using System;
using Imba.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GreiB.GameServices.SaveData
{
    public class SaveDataHandler : ManualSingletonMono<SaveDataHandler>
    {
        public SaveData saveData;

        #region Private Variables

        private const string SaveKey = "UserDataKey";
        private bool _requestSave;

        #endregion

        #region DAILY REWARD

        public void ResetLastTimeEarnedDailyReward()
        {
            saveData.lastTimeEarnedDailyReward = DateTime.Now.ToString();
        }

        public void AddDayDailyRewardClaimed(int day)
        {
            if (!saveData.dailyRewardClaimedList.Contains(day))
            {
                saveData.dailyRewardClaimedList.Add(day);
            }
        }

        public void ResetDailyRewardList()
        {
            saveData.dailyRewardClaimedList.Clear();
        }

        public bool IsDayDailyRewardClaimed(int day)
        {
            if (saveData.dailyRewardClaimedList.Count == 0)
            {
                return false;
            }

            for (int i = 0; i < saveData.dailyRewardClaimedList.Count; i++)
            {
                if (saveData.dailyRewardClaimedList[i] == day)
                {
                    return true;
                }
            }

            return false;
        }

        public DateTime GetLastTimeEarnedDailyReward()
        {
            return DateTime.Parse(saveData.lastTimeEarnedDailyReward);
        }

        #endregion

        #region Unity Methods

        public override void Awake()
        {
            base.Awake();
            OnLoadData();
        }

        private void OnDisable()
        {
            OnSaveData();
        }

        private void OnApplicationPause(bool pause)
        {
            OnSaveData();
        }

        private void OnApplicationQuit()
        {
            OnSaveData();
        }

        private void Update()
        {
            if (_requestSave)
            {
                _requestSave = false;
                OnSaveData();
            }
        }

        #endregion

        #region Private Methods

        private string RandomName()
        {
            int random = Random.Range(0, 10000);
            return "Player" + random;
        }

        private void OnSaveData()
        {
            // SaveDataByES3();
            SaveDataByPlayerPrefs();
        }

        private void OnLoadData()
        {
            // LoadDataByES3();
            LoadDataByPlayerPrefs();
        }

        private void LoadDataByPlayerPrefs()
        {
            var json = PlayerPrefs.GetString(SaveKey, "");
            if (!PlayerPrefs.HasKey(SaveKey))
            {
                saveData = new SaveData();
            }
            else
            {
                var loadUserData = JsonUtility.FromJson<SaveData>(json);
                saveData = loadUserData;
            }
        }

        private void SaveDataByPlayerPrefs()
        {
            string json = JsonUtility.ToJson(saveData);
            PlayerPrefs.SetString(SaveKey, json);
            PlayerPrefs.Save();
        }

        #endregion

        #region Public Method

        public void RequestSave()
        {
            _requestSave = true;
            Debug.Log("Requesting save data");
        }

        #endregion

        #region STAR
        public void UpdateStar(int index, int starCount)
        {
            if(this.saveData.stars.Count - 1 > index)
            {
                this.saveData.stars.Add(starCount);
            } else
            {
                this.saveData.stars[index] = starCount;
            }
        }

        public int GetLevelSelect(int level)
        {
            if (level > this.saveData.stars.Count - 1) return 0;
            return this.saveData.stars[level];
        }
        #endregion
    }
}