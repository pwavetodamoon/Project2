using UnityEngine;

namespace CombatSystem.Helper
{
    public class MonsterAttackerTransform : MonoBehaviour, IGetAttackerTransform
    {
        [SerializeField] private Transform AtkTransform;

        public virtual Transform GetAttackerTransform()
        {
            return AtkTransform;
        }
    }
}