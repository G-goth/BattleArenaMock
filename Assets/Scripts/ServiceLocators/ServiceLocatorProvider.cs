namespace ProjectNJSJ.Assets.Scripts.ServiceLocators
{
    public class ServiceLocatorProvider : SingletonMonoBehaviour<ServiceLocatorProvider>
    {
        public ServiceLocator unityCurrent{ get; private set; }

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected override void Awake()
        {
            // 依存関係を登録
            base.Awake();
        }
    }
}