namespace BattleArenaMock.Assets.Scripts.MonsterProviders
{
    public interface IMonsterPrivider
    {
        // モンスター・通常攻撃
        void MonsterLaunchAttack();
        // モンスター・魔法
        void MonsterUseMagic();
        // モンスター・特殊攻撃
        void MonsterLaunchSpecialAttack();
        // モンスター・特技
        void MonsterUseSkill();
    }
}