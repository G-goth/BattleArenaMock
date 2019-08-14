using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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
            // ReturnTotalScoreArray(statusArray, statusArray.Sum());
            for(int i = 0; i < 20; ++i)
            {
                TestRangeMethod(statusArray, statusArray.Sum());
            }
        }

        private float[] ReturnTotalScoreArray(int[] totalScoreArray, int sum)
        {
            var test = Mathf.RoundToInt(totalScoreArray.Select(totalScore => (float)totalScore / (float)sum * 100).Sum());
            return totalScoreArray.Select(totalScore => (float)totalScore / (float)sum).ToArray();
        }
        private void TestRangeMethod(int[] totalScoreArray, int sum)
        {
            int min = 0, max = 0;
            Dictionary<string, (int min, int max)> testMap = new Dictionary<string, (int min, int max)>();
            
            testMap.Add("Monster1", (0, Mathf.RoundToInt((float)totalScoreArray[0] / (float)sum * 100)));
            for(int i = 1; i < totalScoreArray.Length; ++i)
            {
                min = Mathf.RoundToInt((float)totalScoreArray[i - 1] / (float)sum * 100) + min + 1;
                max = Mathf.RoundToInt((float)totalScoreArray[i] / (float)sum * 100) + min;
                testMap.Add("Monster" + (i + 1), (min, max));
            }

            var randNum = Mathf.RoundToInt(UnityEngine.Random.Range(0.0f, max));
            foreach(KeyValuePair<string, (int min, int max)> item in testMap)
            {
                if(item.Value.min < randNum & item.Value.max > randNum)
                {
                    Debug.Log(item.Key);
                    break;
                }
            }
        }
    }
}