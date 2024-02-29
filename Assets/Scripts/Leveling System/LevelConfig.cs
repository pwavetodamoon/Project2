using Sirenix.OdinInspector;
using UnityEngine;

namespace Leveling_System
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Leveling System/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        void OnEnable()
        {
            if (array == null || array.Length == 0)
                CreateArrayLevel();
        }
        public AnimationCurve curve;
        public int maxLevel = 100;
        public float maxRequiredMoney = 1000;
        private float[] array;
        public int base_cost = 100;
        public float scaleMoney = 1.2f;
        // [Button]
        private float CalculatorMoneyRequired(int level)
        {
            // value in animation curve is 0 - 1
            float levelNormalized = level / (float)maxLevel;
            var moneyRequired = curve.Evaluate(levelNormalized) * maxRequiredMoney;

            return Mathf.RoundToInt(moneyRequired * scaleMoney);
        }
        public float GetMoneyRequired(int level)
        {
            if (array == null)
            {
                Debug.LogError("Array is null, please create array level");
                return 0;
            }
            return array[level - 1];
        }
        [Button]
        void CreateArrayLevel()
        {
            array = new float[maxLevel];
            for (int i = 0; i < maxLevel; i++)
            {
                // Array[i] = MoneyRequird(i + 1, base_cost, scaleMoney);
                array[i] = CalculatorMoneyRequired(i + 1);
            }
        }
    }
}