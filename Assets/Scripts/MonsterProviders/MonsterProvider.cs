using UnityEngine;

namespace BattleArenaMock.Assets.Scripts.MonsterProviders
{
    public class MonsterProvider : IMonsterPrivider
    {
        // モンスター・通常攻撃
        public void MonsterLaunchAttack()
        {
            // Debug.Log("Normal Attack!!");
        }
        // モンスター・魔法
        public void MonsterUseMagic()
        {}
        // モンスター・特殊攻撃
        public void MonsterLaunchSpecialAttack()
        {}
        // モンスター・特技
        public void MonsterUseSkill()
        {}
    }
}