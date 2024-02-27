using System.Collections.Generic;
using NewCombat.Characters;
using UnityEngine;
using UnityEngine.Serialization;

namespace NewCombat.Slots
{
    public class BannedSlotControl : MonoBehaviour
    {
        [SerializeField] public float radius = 0;
        public readonly int SlotIndex = -1;
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radius);
        }

        public void SetHeroIntoStandPosition(Transform hero)
        {
            if (hero == null)
            {
                return;
            }
            hero.gameObject.SetActive(false);
            hero.position = transform.position;

        }

    }
}
