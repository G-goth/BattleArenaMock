using UnityEngine;

namespace BattleArenaMock.Assets.Scripts.Player
{
    public class PlayerWallet : MonoBehaviour
    {
        [SerializeField] private int coin;
        public int CoinAmountProp
        {
            get{ return coin; }
            set{ coin = value; }
        }
        
        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {
            CoinAmountProp = coin;
        }
        private void CoinAddition(int coin)
        {
            CoinAmountProp += coin;
        }
        private void CoinSubtraction(int coin)
        {
            CoinAmountProp -= coin;
        }
    }
}