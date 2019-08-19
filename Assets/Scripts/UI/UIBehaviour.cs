using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using BattleArenaMock.Assets.Scripts.Battle;
using BattleArenaMock.Assets.Scripts.Player;
using BattleArenaMock.Assets.Scripts.Managers.Battlemanagers;

namespace BattleArenaMock.Assets.Scripts.UI
{
    public class UIBehaviour : MonoBehaviour
    {
        [SerializeField] private Canvas battleCanvas;
        [SerializeField] private Text coinAmountText;
        [SerializeField] private Text winningText;
        [SerializeField] private int BetCoinLimit;
        private OddsCalculate oddscalc;
        private PlayerWallet coinAmount;
        private BattlePredict predict;
        private BattleManager manager;
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
            manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<BattleManager>();
            bettingButton = GameObject.FindGameObjectsWithTag("OddsUIs").ToList();
            dropDownBetting = GetComponentInChildren<Dropdown>();

            // 結果テキストの初期化
            winningText.text = "";

            // コイン量の変化によってテキストを変更
            var coinAmountText = this.UpdateAsObservable()
                .Select(_ => coinAmount.CoinAmountProp)
                .DistinctUntilChanged()
                .Subscribe(_ => {
                    UpdateCoinAmount();
                    });

            dropDownBetting.ClearOptions();
            dropDownBetting.AddOptions(Enumerable.Range(1, BetCoinLimit).Select(num => num.ToString()).ToList());
        }

        public void ButtonClick(string monsterName)
        {
            winningText.text = "";
            coinAmount.CoinAmountProp -= (dropDownBetting.value + 1);
            manager.MonsterNameProp = monsterName;
            AllGUIDeactivate();
            manager.LunchCoroutine();
        }
        public void PulldownMenuChanged()
        {
            oddscalc.BettingCoinProp = dropDownBetting.value + 1;
        }
        private void AllGUIDeactivate()
        {
            foreach(var button in bettingButton)
            {
                button.SetActive(false);
            }
        }
        public void AllGUIActive()
        {
            foreach(var button in bettingButton)
            {
                button.SetActive(true);
            }
        }
        public void BattleWinningDisplay(bool result)
        {
            if(!result)
            {
                // 負け
                winningText.text = "負け・・・";
            }
            else
            {
                // 勝ち
                winningText.text = "勝ち！！";
            }
        }
        private void UpdateCoinAmount()
        {
            coinAmountText.text = "あなたの残りコインは" + coinAmount.CoinAmountProp + "コインです。";
        }
    }
}