using CombatSystem.Entity;
using SlotHero;
using UnityEngine;

namespace CombatSystem.Helper
{
    public class HeroAttackerTransform : MonoBehaviour, IGetAttackerTransform
    {
        public HeroCharacter heroCharacter;

        public Transform GetAttackerTransform()
        {
            return SlotManager.Instance.GetAttackerTransform(heroCharacter.InGameSlotIndex);
        }
    }
}