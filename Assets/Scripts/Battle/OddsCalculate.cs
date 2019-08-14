using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BattleArenaMock.Scripts.Monster;
using BattleArenaMock.Assets.Scripts.Managers.Battlemanagers;

namespace BattleArenaMock.Assets.Scripts.Battle
{
    public class OddsCalculate : MonoBehaviour
    {
        private List<GameObject> oddsTextObjectList = new List<GameObject>();
        private List<Text> oddsTextList = new List<Text>();
        private BattleManager battleManager;
        private int[] statusArray = new int[3];
        private UtilityMethods utility = new UtilityMethods();

        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {
            // モンスターのGameObjectを取得
            battleManager = GetComponent<BattleManager>();
            statusArray = battleManager.MonsterObjectListProp.Select(obj => obj.GetComponentInChildren<MonsterStatus>().GetMonsterStatusGroup().TotalScore).ToArray();

            oddsTextObjectList.AddRange(GameObject.FindGameObjectsWithTag("OddsText").OrderBy(go => go.name));
            foreach(var text in oddsTextObjectList)
            {
                oddsTextList.Add(text.GetComponent<Text>());
            }

            // 初起動の時のオッズ
            OddsTextOutPut(OddsCalculating(statusArray));
        }

        public void Ignition()
        {
            OddsTextOutPut(OddsCalculating(statusArray));
        }
        private List<float> OddsCalculating(int[] statusArray)
        {
            float oddsCoefficient = 25.0f;
            List<float> maxDiffList = new List<float>();
            List<float> oddsList = new List<float>();

            foreach(var score in statusArray)
            {
                if((statusArray.Max() - score) < 1)
                {
                    maxDiffList.Add(1);
                }
                else
                {
                    maxDiffList.Add(statusArray.Max() - score);
                }
            }
            
            for(int i = 0; i < statusArray.Length; ++i)
            {
                float odds;
                if((float)statusArray.Max() / (float)statusArray[i] * maxDiffList[i] / oddsCoefficient < 1)
                {
                    odds = 1 + (Random.Range(0.1f, 0.9f) * Random.Range(0.1f, 0.9f));
                }
                else
                {
                    odds = ((float)statusArray.Max() / (float)statusArray[i] * maxDiffList[i] / oddsCoefficient) + (Random.Range(0.1f, 0.9f) * Random.Range(0.1f, 0.9f));
                }
                oddsList.Add(utility.AdvancedFloatRound(odds, 1));
            }

            return oddsList;
        }

        private void OddsTextOutPut(List<float> oddsList)
        {
            for(int i = 0; i < oddsTextList.Count; ++i)
            {
                oddsTextList[i].text = oddsList[i].ToString();
            }
        }
    }
}