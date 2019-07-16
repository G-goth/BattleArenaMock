using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using BattleArenaMock.Scripts.Monster;

namespace BattleArenaMock.Assets.Scripts.Managers.Battlemanagers
{
    public class TurnManager : MonoBehaviour
    {
        // 闘技場出場モンスター系
        private List<GameObject> monsterObjectList = new List<GameObject>();
        private List<GameObject> monsterStatusList = new List<GameObject>();
        private Dictionary<GameObject, MonsterStatus> monsterStatusMap = new Dictionary<GameObject, MonsterStatus>();

        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {
            monsterObjectList.AddRange(GameObject.FindGameObjectsWithTag("Monster").OrderBy(go => go.name));
            foreach(var monstarStatus in monsterObjectList)
            {
                Debug.Log(monstarStatus.GetComponentInChildren<MonsterStatus>().MonsterStatusGroupProp.HP);
            }
        }
    }
}