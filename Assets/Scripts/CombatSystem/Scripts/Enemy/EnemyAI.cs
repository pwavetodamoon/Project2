using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Monster")
        {
            collision.GetComponent<EnemyMoving>().isMoving = false;
        }
            
    }
    [Button]
    public void Test()
    {
        ActionCommand actionCommand = new ActionCommand()
        {
            Time = 1f,
            EndCallbackMethod = () => { Debug.Log("Attack"); }
        };
        attackSequence.AddCommand(actionCommand);
    }
    ActionSequence attackSequence;
    private void Awake()
    {
        attackSequence = GetComponent<ActionSequence>();
    }
}