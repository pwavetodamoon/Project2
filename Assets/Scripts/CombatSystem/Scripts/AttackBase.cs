using UnityEngine;



public class AttackBase : MonoBehaviour
{

    [SerializeField] public Enemy enemyData;
    [SerializeField] public Player playerData;
    [SerializeField] public BaseData baseData;

    protected bool IsAttack = false;
}

