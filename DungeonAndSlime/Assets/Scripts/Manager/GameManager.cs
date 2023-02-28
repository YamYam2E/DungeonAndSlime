using Actor;
using Common;
using Data;
using DG.Tweening;
using GameTable;
using UnityEngine;

namespace Manager
{
    public class GameManager : MonoBehaviourSingleton<GameManager>
    {
        [SerializeField] private Transform respawnLocation;
        [SerializeField] private MainPlayer mainPlayer;
        
        /// <summary>
        /// 게임 데이터
        /// </summary>
        private GameData gameData = new GameData();

        protected override void Initialize()
        {
            base.Initialize();

            Application.targetFrameRate = 60;
            
            DOTween.Init(true);
            
            TableManager.Instance.Initialize();
            
            ObjectPoolManager.Instance.Initialize();
            
            LocalStorage.LoadPlayerData();
        }

        public ActorStatus MainPlayerStatus => mainPlayer.Status;
    }
}