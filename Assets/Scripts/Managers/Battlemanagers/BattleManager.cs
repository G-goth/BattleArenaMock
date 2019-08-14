using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using BattleArenaMock.Scripts.Monster;

namespace BattleArenaMock.Assets.Scripts.Managers.Battlemanagers
{
    public class BattleManager : MonoBehaviour, IBattleManagerReciever
    {
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

        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {
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
            Debug.Log("PostMessageOnRecieve");
        }
    }
}