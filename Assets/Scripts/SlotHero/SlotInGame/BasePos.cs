using CombatSystem.Entity;
using SlotHero.SlotInGame;
using UnityEngine;

public class BasePos : MonoBehaviour
{

    //[SerializeField] public float radius;
    public readonly int SlotIndexs = -1;
    public float a, b;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, new Vector3(a, b, 0));

    }
    
}
