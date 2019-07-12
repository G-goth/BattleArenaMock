using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace BattleArenaMock.Assets.Scripts.Battle
{
    public class ActionOrder : MonoBehaviour
    {
        // モンスターのリスト
        private List<GameObject> monsterObject = new List<GameObject>();
        
        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {
        }
    }
}