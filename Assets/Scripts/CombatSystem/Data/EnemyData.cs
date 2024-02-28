using NewCombat.HeroDataManager.Data;
using UnityEngine;

namespace CombatSystem.Data
{
    //DATA cua quai
    [CreateAssetMenu(fileName = "EnemyData Data", menuName = "Scriptable Object/ EnemyData")]
    public class EnemyData : HeroData
    {
        public float speed;
        public EnemyType enemy_type;
    }
    public enum EnemyType
    { goblin, elf, monster, bat }
}