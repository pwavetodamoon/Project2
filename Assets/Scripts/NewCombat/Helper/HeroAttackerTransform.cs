using NewCombat.Characters;
using NewCombat.Slots;
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