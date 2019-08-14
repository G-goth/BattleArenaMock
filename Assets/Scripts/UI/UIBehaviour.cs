using UnityEngine;
using UnityEngine.UI;
using BattleArenaMock.Assets.Scripts.Battle;

namespace BattleArenaMock.Assets.Scripts.UI
{
    public class UIBehaviour : MonoBehaviour
    {
        [SerializeField] private Canvas battleCanvas;
        private OddsCalculate oddscalc;

        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {
            oddscalc = GameObject.FindGameObjectWithTag("GameController").GetComponent<OddsCalculate>();
        }

        public void ButtonClick()
        {
            oddscalc.Ignition();
        }
    }
}