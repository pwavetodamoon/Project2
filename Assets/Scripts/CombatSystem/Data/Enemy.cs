using UnityEngine;
//DATA cua quai
[CreateAssetMenu(fileName = "Enemy Data", menuName = "Scriptable Object/ Enemy")]
public class Enemy : BaseData
{
    public Transform enemyPos;
    public EnemyType enemy_type;
}
public enum EnemyType
{ goblin, elf, monster, bat }


