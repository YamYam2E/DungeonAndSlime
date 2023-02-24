using System.Collections.Generic;
using Common;
using GameTable;
using UnityEngine;

namespace Manager
{
    public class GameManager : MonoBehaviourSingleton<GameManager>
    {
        [SerializeField] private Transform respawnLocation;

        protected override void Initialize()
        {
            base.Initialize();
            
            TableManager.Instance.Initialize();
            
            
        }
    }
}