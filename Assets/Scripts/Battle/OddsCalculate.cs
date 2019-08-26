using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BattleArenaMock.Scripts.Monster;
using BattleArenaMock.Assets.Scripts.Player;
using BattleArenaMock.Assets.Scripts.Managers.Battlemanagers;
using BattleArenaMock.Assets.Scripts.UI;

namespace BattleArenaMock.Assets.Scripts.Battle
{
    public class OddsCalculate : MonoBehaviour
    {
        private List<GameObject> oddsTextObjectList = new List<GameObject>();
        private List<Text> oddsTextList = new List<Text>();
        private BattleManager battleManager;
        private int[] statusArray = new int[3];
        private UtilityMethods utility = new UtilityMethods();
        private BattlePredict predict;
        private PlayerWallet wallet;
        private UIBehaviour uiBehaviour;

        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {
            // プレイヤー(配信者)のコイン量
            wallet = GetComponent<PlayerWallet>();
            // 勝ち残ったモンスター判定
            predict = GetComponent<BattlePredict>();
            // モンスターのGameObjectを取得
            battleManager = GetComponent<BattleManager>();
            statusArray = battleManager.MonsterObjectListProp.Select(obj => obj.GetComponentInChildren<MonsterStatus>().GetMonsterStatusGroup().TotalScore).ToArray();

            oddsTextObjectList.AddRange(GameObject.FindGameObjectsWithTag("OddsText").OrderBy(go => go.name));
            foreach(var text in oddsTextObjectList)
            {
                oddsTextList.Add(text.GetComponent<Text>());
            }

            // 初期賭けコイン
            BettingCoinProp = 1;
            // 初起動の時のオッズ
            OddsTextOutPut(OddsCalculating(statusArray));
            SimpleOddsCalclation(statusArray);
        }

        public int BettingCoinProp{ get; set; }
        public bool Ignition(string monsterName)
        {
            // オッズの表示の更新
            OddsTextOutPut(OddsCalculating(statusArray));
            // モンスターの名とオッズの組を作る
            var oddsMap = OddsCoefficientMap(OddsCalculating(statusArray));
            // 当落の判定(計算もここでしちゃってるから別に分けたい)
            if(!predict.PredictMonsterNameProp.Contains(monsterName))
            {
                // 何もしない
                return false;
            }
            else
            {
                RefundAmount(oddsMap, monsterName);
                return true;
            }
        }

        // 払い戻しコインの取得
        public int RefundAmountProp{ get; set; }
        // オッズに基づいた払い戻し金額の計算
        private void RefundAmount(Dictionary<string, float> oddsMap, string monsterName)
        {
            foreach(var hit in oddsMap)
            {
                if(hit.Key != monsterName)
                {
                    // 何もしない
                }
                else
                {
                    float answer = BettingCoinProp * hit.Value;
                    RefundAmountProp = Mathf.RoundToInt(answer);
                    wallet.CoinAmountProp += Mathf.RoundToInt(answer);
                }
            }
        }
        
        // 単純な能力値の総合値からオッズの計算をする
        private void SimpleOddsCalclation(int[] totalScoreArray)
        {
            int[] stepped = new int[3];
            string[] monsterNameArray = new string[]{"Monster1", "Monster2", "Monster3", "Monster4"};
            var sum = totalScoreArray.Sum();
            var weightArray = totalScoreArray.Select(weight => Mathf.RoundToInt(((float)weight / (float)sum) * 100)).ToArray();
        }

        // オッズの算出
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

        // オッズとモンスター名の組を作る
        private Dictionary<string, float> OddsCoefficientMap(List<float> oddsList)
        {
            int number = 1;
            Dictionary<string, float> oddsMap = new Dictionary<string, float>();
            foreach(var odds in oddsList)
            {
                oddsMap.Add("Monster" + number, odds);
                ++number;
            }
            return oddsMap;
        }

        // 計算されたオッズの表示
        private void OddsTextOutPut(List<float> oddsList)
        {
            for(int i = 0; i < oddsTextList.Count; ++i)
            {
                oddsTextList[i].text = oddsList[i].ToString();
            }
        }
    }
}