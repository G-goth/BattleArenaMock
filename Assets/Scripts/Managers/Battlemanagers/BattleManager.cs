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
        [SerializeField] private MonsterStatus monsStatus = default;
        private List<GameObject> monsterObjectList = new List<GameObject>();

        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {
            List<MonsterStatus> status = new List<MonsterStatus>();
            monsterObjectList.AddRange(GameObject.FindGameObjectsWithTag("Monster").OrderBy(go => go.name));
            foreach(var monstarStatus in monsterObjectList)
            {
                status.Add(monstarStatus.GetComponentInChildren<MonsterStatus>());
            }

            var testUpdate = this.UpdateAsObservable()
                .Subscribe(_ => {
                    Debug.Log(status[0].MonsterStatusGroupProp.HP);
                    status[0].UpdateMonsterStatusStruct();
                });
        }
        // モンスターの死亡処理
        // モンスターのすばやさ順の行動処理 すばやさ順にモンスター行動のメソッドを呼ぶ感じにしたい
    }
}