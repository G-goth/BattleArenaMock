using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using BattleArenaMock.Assets.Scripts.Battle;
using BattleArenaMock.Assets.Scripts.Player;

namespace BattleArenaMock.Assets.Scripts.UI
{
    public class UIBehaviour : MonoBehaviour
    {
        [SerializeField] private Canvas battleCanvas;
        [SerializeField] private Text coinAmountText;
        private OddsCalculate oddscalc;
        private PlayerWallet coinAmount;
        private BattlePredict predict;

        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {
            coinAmount = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerWallet>();
            oddscalc = GameObject.FindGameObjectWithTag("GameController").GetComponent<OddsCalculate>();
            predict = GameObject.FindGameObjectWithTag("GameController").GetComponent<BattlePredict>();

            // コイン量の変化によってテキストを変更
            var coinAmountText = this.UpdateAsObservable()
                .Select(_ => coinAmount.CoinAmountProp)
                .DistinctUntilChanged()
                .Subscribe(_ => {
                    UpdateCoinAmount();
                    });
        }

        public void ButtonClick(string monsterName)
        {
            oddscalc.Ignition(monsterName);
        }
        private void UpdateCoinAmount()
        {
            coinAmountText.text = "あなたの残りコインは" + coinAmount.CoinAmountProp + "コインです。";
        }
    }
}