using Sirenix.OdinInspector;
using UnityEngine;

public class CharactersBase : MonoBehaviour
{
    [SerializeField] private AttackMethod normalAttack;
    public Transform AttackedPosition;
    public CharactersBase Enemy;
    public CharacterSlot Slot;
    public Animator_Base Animator;
    private void Awake()
    {
        Slot = GetComponentInParent<CharacterSlot>();
    }
    private void Start()
    {
        transform.position = Slot.GetCharacterPosition();
    }
    [Button]
    private void InvokeEvent()
    {
        normalAttack.StartAttack(this, Enemy.AttackedPosition.position);
    }
    public void Attack()
    {
        //Animator.ChangeAnimation(Human_Animator.);
    }
}

