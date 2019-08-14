using System;

namespace BattleArenaMock.Assets.Scripts
{
    public class UtilityMethods
    {
        private static readonly int TEN = 10;
        
        public float AdvancedFloatRound(float num, int pointPosition)
        {
            if(pointPosition <= 0) return 1.0f;
            return (float)Math.Round(num * (float)Math.Pow(TEN, pointPosition)) / (float)Math.Pow(TEN, pointPosition);
        }
    }
}