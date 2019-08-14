using UnityEngine.EventSystems;

namespace BattleArenaMock.Assets.Scripts.Managers.Battlemanagers
{
    interface IBattleManagerReciever : IEventSystemHandler
    {
        void PostMessageOnRecieve();
    }

    interface IBattlePredictReciever : IEventSystemHandler
    {
        void StartBattleStream();
    }
}