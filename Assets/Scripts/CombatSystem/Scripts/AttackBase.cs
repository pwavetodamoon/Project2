using UnityEngine;



public class AttackBase : MonoBehaviour
{

    [SerializeField] public Enemy enemyData;
    [SerializeField] public Player playerData;
    protected bool IsAttack = false;
}

