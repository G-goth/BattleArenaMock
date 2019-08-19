using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using BattleArenaMock.Scripts.Monster;
using BattleArenaMock.Assets.Scripts.UI;
using BattleArenaMock.Assets.Scripts.Battle;

namespace BattleArenaMock.Assets.Scripts.Managers.Battlemanagers
{
    public class BattleManager : MonoBehaviour, IBattleManagerReciever
    {
        [SerializeField] private Text winningText;
        private UIBehaviour battleUI;
        private OddsCalculate oddscalc;
        // 闘技場出場モンスター系
        private List<GameObject> monsterObjectList = new List<GameObject>();
        private Dictionary<string, MonsterStatusGroup> monsterObjectMap = new Dictionary<string, MonsterStatusGroup>();
        private List<MonsterStatus> status = new List<MonsterStatus>();

        // プロパティ
        public List<GameObject> MonsterObjectListProp
        {
            get{ return monsterObjectList; }
            private set{ monsterObjectList = value; }
        }
        public string MonsterNameProp{ get; set;}

        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {
            battleUI = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIBehaviour>();
            oddscalc = GameObject.FindGameObjectWithTag("GameController").GetComponent<OddsCalculate>();
            // 各ダミーのモンスターデータからステータスクラスを取得
            monsterObjectList.AddRange(GameObject.FindGameObjectsWithTag("Monster").OrderBy(go => go.name));
            foreach(var monstarStatus in monsterObjectList)
            {
                status.Add(monstarStatus.GetComponentInChildren<MonsterStatus>());
            }
            
            // プロパティへ代入
            MonsterObjectListProp = monsterObjectList;
        }
        // モンスターの死亡処理
        // モンスターのすばやさ順にモンスターの振る舞いクラスごとリストに追加する
        private List<int> AgilityListAddingStatus()
        {
            List<int> agilityList = new List<int>();
            Dictionary<string, int> agilityMap = new Dictionary<string, int>();
            foreach(var agility in status)
            {
                agilityList.Add(agility.MonsterStatusGroupProp.Agility);
            }
            
            agilityList.Sort();
            agilityList.Reverse();

            return agilityList;
        }
        // モンスターのすばやさ順の行動処理 すばやさ順にモンスター行動のメソッドを呼ぶ感じにしたい

        // メッセージの受け口(仮)
        public void PostMessageOnRecieve()
        {
        }
        
        // StartCoroutineをスタートするメソッド
        public void LunchCoroutine()
        {
            string methodName = new Func<IEnumerator>(DammiyBattleTime).Method.Name;
            StartCoroutine(methodName);
        }
        // バトルしてる感じのダミー時間のメソッド
        private IEnumerator DammiyBattleTime()
        {
            winningText.text = "バトル中・・・";
            yield return new WaitForSeconds(3.0f);
            winningText.text = "";
            battleUI.BattleWinningDisplay(oddscalc.Ignition(MonsterNameProp));
            battleUI.AllGUIActive();
        }
    }
}