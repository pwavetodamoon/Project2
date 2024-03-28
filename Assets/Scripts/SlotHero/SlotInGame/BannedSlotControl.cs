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

        public void SetHeroIntoStandPosition(HeroCharacter hero)
        {
            if (hero == null) return;
            hero.GetEntityAction().OnHealthChange = null;
            //hero.gameObject.SetActive(false);
            hero.transform.position = transform.position;
            AudioManager.Instance.PlaySFX("Remove Champion");
        }
    }
}

public class SlotInGameBase : MonoBehaviour
{
    [SerializeField] public float radius;
    public readonly int SlotIndex = -1;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    public virtual void SetHeroIntoStandPosition(HeroCharacter hero)
    {
        if (hero == null) return;
        hero.GetComponent<EntityCharacter>().GetEntityAction().OnHealthChange = null;
        //hero.gameObject.SetActive(false);
        hero.transform.position = transform.position;
        AudioManager.Instance.PlaySFX("Remove Champion");
    }
}