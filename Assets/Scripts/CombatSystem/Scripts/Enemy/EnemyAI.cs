using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
            
    }
    [Button]
    public void Test()
    {
        ActionCommand actionCommand = new ActionCommand()
        {
            time = 1f,
            action = () => { Debug.Log("Attack"); }
        };
        attackSequence.AddCommand(actionCommand);
    }
    AttackSequence attackSequence;
    private void Awake()
    {
        attackSequence = GetComponent<AttackSequence>();
    }
}