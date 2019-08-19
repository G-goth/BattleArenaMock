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
        [SerializeField] private Text bettingText;
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

            dropDownBetting.AddOptions(Enumerable.Range(1, BetCoinLimit).Select(num => num.ToString()).ToList());
            // 選んだオッズ
            bettingText.enabled = false;
        }

        public void ButtonClick(string monsterName)
        {
            // 当落の表示の初期化
            winningText.text = "";
            // 選択したモンスターのオッズの表示
            SelectedMonsterOdds(monsterName);
            // 使ったコインをプレイヤーのウォレットから引く
            coinAmount.CoinAmountProp -= (dropDownBetting.value + 1);
            // どのモンスターが勝ったかの判定用
            manager.MonsterNameProp = monsterName;
            // すべてのボタンUIを非アクティブ化
            AllGUIDeactivate();
            // バトル時間っぽいコルーチンを起動
            manager.LunchCoroutine();
        }

        // プルダウンメニューに変化があったらプロパティへ代入
        public void PulldownMenuChanged()
        {
            oddscalc.BettingCoinProp = dropDownBetting.value + 1;
        }

        // すべてのボタン系UIを非アクティブ化
        private void AllGUIDeactivate()
        {
            foreach(var button in bettingButton)
            {
                button.SetActive(false);
            }
        }
        // すべてのボタン系UIをアクティブ化
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
                bettingText.enabled = false;
                winningText.text = "負け・・・";
            }
            else
            {
                // 勝ち
                bettingText.enabled = false;
                winningText.text = oddscalc.RefundAmountProp + "コイン" + "勝ち！！";
            }
        }
        private void SelectedMonsterOdds(string monsterName)
        {
            bettingText.enabled = true;
            bettingText.text = monsterName;
        }
        private void UpdateCoinAmount()
        {
            coinAmountText.text = "あなたの残りコインは" + coinAmount.CoinAmountProp + "コインです。";
        }
    }
}