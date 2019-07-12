using System.Collections.Generic;
using UnityEngine;

namespace BattleArenaMock.Scripts.Monster
{
    public struct MonsterStatusGroup
    {
        // HP
        public int HP{ get; private set; }
        // MP
        public int MP{ get; private set; }
        // 攻撃力
        public int Strength{ get; private set; }
        // 防御力
        public int Defence{ get; private set; }
        // すばやさ
        public int Agility{ get; private set; }

        public MonsterStatusGroup(int hp, int mp, int strength, int defence, int agility)
        {
            HP = hp;
            MP = mp;
            Strength = strength;
            Defence = defence;
            Agility = agility;
        }
    }
    public class MonsterStatus : MonoBehaviour
    {
        // モンスター関連
        [SerializeField] private int hp;
        [SerializeField] private int mp;
        [SerializeField] private int strength;
        [SerializeField] private int defence;
        [SerializeField] private int agility;
        // モンスターに必要最低限のデータ構造体
        private MonsterStatusGroup monsStatus;
        public MonsterStatusGroup MonsterStatusGroupProp{ get; private set; }
        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {
            MonsterStatusGroupProp = new MonsterStatusGroup(hp, mp, strength, defence, agility);
        }
    }
}