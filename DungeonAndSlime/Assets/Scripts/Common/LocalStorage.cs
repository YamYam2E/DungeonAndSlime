using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Data;
using Enum;
using Manager;
using UnityEngine;

namespace Common
{
    public abstract class LocalStorage : Singleton<LocalStorage>
    {
        public static void SavePlayerData()
        {
            var binaryFormatter = new BinaryFormatter();

            using var memoryStream = new MemoryStream();
            
            binaryFormatter.Serialize(memoryStream, GameManager.Instance.MainPlayerStatus);

            var data = Convert.ToBase64String(memoryStream.ToArray());
                
            PlayerPrefs.SetString(EPlayerData.PlayerStatus.ToString(), data);
        }
        
        public static void LoadPlayerData()
        {
            var data = PlayerPrefs.GetString(EPlayerData.PlayerStatus.ToString(), string.Empty);

            if (data == string.Empty)
                return;

            using var memoryStream = new MemoryStream(Convert.FromBase64String(data));
            
            var binaryFormatter = new BinaryFormatter();
            var convertData = binaryFormatter.Deserialize(memoryStream);

            GameManager.Instance.MainPlayerStatus.SetData(convertData as ActorStatus);
        }
    }
}