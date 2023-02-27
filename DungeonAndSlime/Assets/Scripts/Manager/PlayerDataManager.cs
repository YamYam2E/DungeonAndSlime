using Common;
using Enum;
using UnityEngine;

namespace Manager
{
    public class PlayerDataManager : Singleton<PlayerDataManager>
    {
        public void Save(EPlayerData key, int value)
            => PlayerPrefs.SetInt(key.ToString(), value);
        
        public void Save(EPlayerData key, float value)
            => PlayerPrefs.SetFloat(key.ToString(), value);
        
        public void Save(EPlayerData key, string value)
            => PlayerPrefs.SetString(key.ToString(), value);

        public int LoadInt(EPlayerData key)
            => PlayerPrefs.GetInt(key.ToString());
        
        public float LoadFloat(EPlayerData key)
            => PlayerPrefs.GetFloat(key.ToString());
        
        public string LoadString(EPlayerData key)
            => PlayerPrefs.GetString(key.ToString());
    }
}