using BattleArenaMock.Assets.Scripts.MonsterProviders;

namespace BattleArenaMock.Assets.Scripts.ServiceLocators
{
    public class ServiceLocatorProvider : SingletonMonoBehaviour<ServiceLocatorProvider>
    {
        public ServiceLocator monsterStatusCurrent{ get; private set; }

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected override void Awake()
        {
            // 依存関係を登録
            base.Awake();
            monsterStatusCurrent = new ServiceLocator();
            monsterStatusCurrent.Register<IMonsterPrivider>(new MonsterProvider());
        }
    }
}