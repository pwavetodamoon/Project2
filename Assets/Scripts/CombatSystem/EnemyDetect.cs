using Helper;
using NewCombat.Characters;
using UnityEngine;

public class EnemyDetect : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsColliderHaveEnemyTag(collision)) return;
        if (collision.TryGetComponent(out IEntity entityCharacter)) entityCharacter.RegisterObject();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!IsColliderHaveEnemyTag(collision)) return;
        if (collision.TryGetComponent(out IEntity entityCharacter)) entityCharacter.ReleaseObject();
    }

    private bool IsColliderHaveEnemyTag(Collider2D collider)
    {
        return collider.gameObject.CompareTag(GameTag.Enemy);
    }
}