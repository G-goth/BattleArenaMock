using UnityEngine;
using BattleArenaMock.Assets.Scripts.MonsterProviders;
using BattleArenaMock.Assets.Scripts.ServiceLocators;

namespace BattleArenaMock.Assets.Scripts.Monster
{
    public class MonsterActionBehaviour : MonoBehaviour
    {
        private IMonsterPrivider monsterProvider;

        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {
            // 登録された依存関係を使用する
            monsterProvider = ServiceLocatorProvider.Instance.monsterStatusCurrent.Resolve<IMonsterPrivider>();
            monsterProvider.MonsterLaunchAttack();
        }
    }
}