using System.Collections;
using UnityEngine;

public class AttackMethod : MonoBehaviour
{
    [SerializeField] float timeCounterForAttack = 4;
    public bool isActive = false;
    Vector3 target;
    CharactersBase character;
    public float speed = 1;
    public void StartAttack(CharactersBase character, Vector3 enemy)
    {
        if (isActive) return;
        this.character = character;
        this.target = enemy;
        isActive = true;
        StartCoroutine(StartBehavior());
    }
    private IEnumerator StartBehavior()
    {
        yield return MoveToTransform(character.transform, target);
        yield return MoveToTransform(character.transform,character.Slot.GetCharacterPosition());
    }

    private IEnumerator MoveToTransform(Transform Character, Vector3 TargetPosition)
    {
        while (true)
        {
            var direction = TargetPosition - Character.position;
            direction.Normalize();
            Character.Translate(speed * Time.deltaTime * direction);
            if(Vector3.Distance(Character.position, TargetPosition) < 0.1f)
            {
                Debug.Log("MoveToTransform: " + Vector3.Distance(Character.position, TargetPosition));
                break;
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
