using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "MoneyConfig", menuName = "Leveling System/MoneyConfig")]
// Create money config for monster drop increase by curve and scale
public class MoneyConfig : ScriptableObject
{

    public float min;
    public float max;
    public AnimationCurve curve;
    public int maxLevel = 100;
    public float maxMoney = 1000;
    public int baseMoney = 100;
    public float scaleMoney = 1.2f;
    private float[] array;

    private void OnEnable()
    {
        if (array == null || array.Length == 0)
            CreateArrayLevel();
    }

    [Button]
    public int Get(int level)
    {
        // value in animation curve is 0 - 1
        var levelNormalized = level / (float)maxLevel;
        var money = curve.Evaluate(levelNormalized) * maxMoney;
        var newMoney = Mathf.RoundToInt(money * scaleMoney);
        return RandomByPercent(min, max, money);
    }

    [Button]
    private void CreateArrayLevel()
    {
        array = new float[maxLevel]; //mang level toi da la 100
        for (var i = 0; i < maxLevel; i++)
            // Array[i] = MoneyRequird(i + 1, base_cost, scaleMoney);
            array[i] = Get(i + 1);
    }
    private int RandomByPercent(float min, float max, float value)
    {
        var _value = Random.Range(min, max) * value;
        return Mathf.RoundToInt(_value);
    }

}