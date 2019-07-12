using System.Collections.Generic;
using UnityEngine;
using BattleArenaMock.Scripts.Monster;

namespace BattleArenaMock.Assets.Scripts.Managers.Battlemanagers
{
    public class BattleManager : MonoBehaviour
    {
        [SerializeField] private MonsterStatus monsStatus = (default);

        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {
            Debug.Log("Monster1 MP is " + monsStatus.MonsterStatusGroupProp.HP);
        }
    }
}