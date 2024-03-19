using CombatSystem.Entity;
using UnityEngine;

namespace SlotHero.SlotInGame
{
    public class BannedSlotControl : MonoBehaviour
    {
        [SerializeField] public float radius;
        public readonly int SlotIndex = -1;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radius);
        }

        public void SetHeroIntoStandPosition(Transform hero)
        {
            if (hero == null) return;
            hero.GetComponent<EntityCharacter>().GetEntityStats().OnHealthChange = null;
            //hero.gameObject.SetActive(false);
            hero.position = transform.position;
            AudioManager.Instance.PlaySFX("Remove Champion");
        }
    }
}