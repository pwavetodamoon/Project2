using UnityEngine;

namespace CombatSystem.Data
{
    //DATA cua quai
    [CreateAssetMenu(fileName = "EnemyData Data", menuName = "Scriptable Object/ EnemyData")]
    public class EnemyData : BaseData
    {
        public float speed;
        public EnemyType enemy_type;
    }
    public enum EnemyType
    { goblin, elf, monster, bat }
}