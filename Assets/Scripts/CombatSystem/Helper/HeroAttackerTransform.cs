using CombatSystem.Entity;
using SlotHero;
using UnityEngine;

namespace CombatSystem.Helper
{
    public class HeroAttackerTransform : MonoBehaviour, IGetAttackerTransform
    {
        public Transform GetAttackerTransform()
        {
            var index = GetComponent<HeroCharacter>().InGameSlotIndex;
            return SlotManager.Instance.GetAttackerTransform(index);
        }
    }
}