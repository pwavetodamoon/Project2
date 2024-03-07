using NewCombat.Entity;
using SlotHero;
using UnityEngine;

namespace NewCombat.Helper
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