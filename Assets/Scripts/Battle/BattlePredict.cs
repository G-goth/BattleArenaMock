using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using BattleArenaMock.Scripts.Monster;
using BattleArenaMock.Assets.Scripts.Managers.Battlemanagers;

namespace BattleArenaMock.Assets.Scripts.Battle
{
    public class BattlePredict : MonoBehaviour
    {
        private BattleManager battleManager;
        private int[] statusArray = new int[3];
        
        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {
            // モンスターのGameObjectを取得
            battleManager = GetComponent<BattleManager>();
            statusArray = battleManager.MonsterObjectListProp.Select(obj => obj.GetComponentInChildren<MonsterStatus>().GetMonsterStatusGroup().TotalScore).ToArray();
            WeightedOddsCalculation(OddsCalculation(statusArray));
        }

        public string PredictMonsterNameProp
        {
            get
            {
                var weightedOdds = WeightedOddsCalculation(OddsCalculation(statusArray));
                return WeightedLotteryMonsterName(statusArray, weightedOdds);
            }
        }
        private Dictionary<string, float> OddsCalculation(int[] totalScoreArray)
        {
            string[] monsterNameArray = new string[]{"Monster1", "Monster2", "Monster3", "Monster4"};
            Dictionary<string, float> oddsMap = new Dictionary<string, float>(){{"Monster1", 0.0f}, {"Monster2", 0.0f}, {"Monster3", 0.0f}, {"Monster4", 0.0f}};
            List<string> keyList = new List<string>(oddsMap.Keys);
            var max = totalScoreArray.Max();
            for(int i = 0; i < oddsMap.Keys.Count; ++i)
            {
                float difference;
                if(max - totalScoreArray[i] < 1.0f)
                {
                    oddsMap[keyList[i]] = 1.0f;
                }
                else
                {
                    difference = (float)(max - totalScoreArray[i]);
                    oddsMap[keyList[i]] = (float)max / (float)totalScoreArray[i] * difference / 25.0f;
                }
            }
            return oddsMap;
        }
        private Dictionary<string, int> WeightedOddsCalculation(Dictionary<string, float> oddsMap)
        {
            Dictionary<string, int> weightedOdds = new Dictionary<string, int>(){{"Monster1", 0}, {"Monster2", 0}, {"Monster3", 0}, {"Monster4", 0}};
            List<string> keyList = new List<string>(oddsMap.Keys);
            for(int i = 0; i < oddsMap.Keys.Count; ++i)
            {
                weightedOdds[keyList[i]] = (int)(1.0f / oddsMap[keyList[i]] * 100);
            }
            return weightedOdds;
        }
        private string WeightedLotteryMonsterName(int[] totalScoreArray, Dictionary<string, int> weightedOdds)
        {
            string monsterName = "";
            string[] monsterNameArray = new string[]{"Monster1", "Monster2", "Monster3", "Monster4"};
            var sum = totalScoreArray.Sum();
            var weightArray = weightedOdds.Select(weight => weight.Value).ToArray();
            var rand = (int)Random.Range(0.0f, weightArray.Sum());
            for(int i = 0; i < totalScoreArray.Length; ++i)
            {
                if(rand < weightArray[i])
                {
                    monsterName = monsterNameArray[i];
                    break;
                }
                rand -= weightArray[i];
            }
            Debug.Log(monsterName);
            return monsterName;
        }
    }
}