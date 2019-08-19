using System.Collections.Generic;
using System.Linq;
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
        [SerializeField] private int BetCoinLimit;
        private OddsCalculate oddscalc;
        private PlayerWallet coinAmount;
        private BattlePredict predict;
        private List<GameObject> bettingButton;
        private Dropdown dropDownBetting;

        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {
            coinAmount = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerWallet>();
            oddscalc = GameObject.FindGameObjectWithTag("GameController").GetComponent<OddsCalculate>();
            predict = GameObject.FindGameObjectWithTag("GameController").GetComponent<BattlePredict>();
            bettingButton = GameObject.FindGameObjectsWithTag("OddsUIs").ToList();
            dropDownBetting = GetComponentInChildren<Dropdown>();

            // コイン量の変化によってテキストを変更
            var coinAmountText = this.UpdateAsObservable()
                .Select(_ => coinAmount.CoinAmountProp)
                .DistinctUntilChanged()
                .Subscribe(_ => {
                    UpdateCoinAmount();
                    });

            // bettingButton[0].SetActive(false);
            dropDownBetting.ClearOptions();
            dropDownBetting.AddOptions(Enumerable.Range(1, BetCoinLimit).Select(num => num.ToString()).ToList());
        }

        public void ButtonClick(string monsterName)
        {
            coinAmount.CoinAmountProp -= (dropDownBetting.value + 1);
            oddscalc.Ignition(monsterName);
        }
        public void PulldownMenuChanged()
        {
            oddscalc.BettingCoinProp = dropDownBetting.value + 1;
        }
        private void UpdateCoinAmount()
        {
            coinAmountText.text = "あなたの残りコインは" + coinAmount.CoinAmountProp + "コインです。";
        }
        private void AllGUIDeactivate()
        {
        }
    }
}