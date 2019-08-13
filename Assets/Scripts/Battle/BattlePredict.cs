using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using BattleArenaMock.Scripts.Monster;
using BattleArenaMock.Assets.Scripts.Managers.Battlemanagers;

namespace BattleArenaMock.Assets.Scripts.Battle
{
    public class BattlePredict : MonoBehaviour
    {
        // モンスターのリスト
        [SerializeField] private List<GameObject> monsterObject = new List<GameObject>();
        private BattleManager battleManager;
        
        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {
            // モンスターのGameObjectを取得
            battleManager = GetComponent<BattleManager>();
            var statusArray = battleManager.MonsterObjectListProp.Select(obj => obj.GetComponentInChildren<MonsterStatus>().GetMonsterStatusGroup().TotalScore).ToArray();
            ReturnTotalScoreArray(statusArray, statusArray.Sum());
        }

        private float[] ReturnTotalScoreArray(int[] totalScoreArray, int sum)
        {
            // Debug.Log((float)totalScoreArray[0] / (float)sum);
            return totalScoreArray.Select(totalScore => (float)totalScore / (float)sum).ToArray();
        }
    }
}