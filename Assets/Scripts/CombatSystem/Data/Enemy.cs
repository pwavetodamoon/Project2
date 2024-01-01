using UnityEditor;
using UnityEngine;
//DATA cua quai
[CreateAssetMenu(fileName = "Enemy Data", menuName = "Scriptable Object/ Enemy")]
public class Enemy : BaseData
{
    public float speed;
    public EnemyType enemy_type;
}
public enum EnemyType
{ goblin, elf, monster, bat }


