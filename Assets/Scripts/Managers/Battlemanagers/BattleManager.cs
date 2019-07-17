using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using BattleArenaMock.Scripts.Monster;

namespace BattleArenaMock.Assets.Scripts.Managers.Battlemanagers
{
    public class BattleManager : MonoBehaviour
    {
        // 闘技場出場モンスター系
        private List<GameObject> monsterObjectList = new List<GameObject>();
        private List<MonsterStatus> status = new List<MonsterStatus>();
        private List<int> agilityList = new List<int>();

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
        }
        // モンスターの死亡処理
        // モンスターのすばやさ順にモンスターの振る舞いクラスごとリストに追加する
        private void AgilityListAddingStatus()
        {
        }
        // モンスターのすばやさ順の行動処理 すばやさ順にモンスター行動のメソッドを呼ぶ感じにしたい
    }
}