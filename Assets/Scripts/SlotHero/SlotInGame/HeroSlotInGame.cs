using CombatSystem.Entity;
using Model.Hero;
using UnityEngine;

namespace SlotHero.SlotInGame
{
    [RequireComponent(typeof(ShadowColor))]
    public class HeroSlotInGame : MonoBehaviour
    {
        [SerializeField] private Transform characterStand;
        public Transform enemyStand;
        public HeroCharacter currentHero;
        public float radius;
        public int SlotIndex;
        private HealthBarStatic healthBar;
        private ShadowColor ShadowColor;

        private Character_Body_Sprites sprites;

        private void Awake()
        {
            ShadowColor = GetComponent<ShadowColor>();
        }
        public void SetHealthBar(HealthBarStatic healthBar)
        {
            this.healthBar = healthBar;
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(characterStand.position, radius);
        }

        public void SetHeroIntoStandPosition(HeroCharacter hero)
        {
            currentHero = hero;
            if (currentHero != null)
            {
                var entityStats = currentHero.GetEntityStats();
                entityStats.OnHealthChange = healthBar.SetHealthBar;
                entityStats.ChangeHealthEvent();

                currentHero.transform.position = characterStand.position;
                sprites = currentHero.GetComponentInChildren<Character_Body_Sprites>();
                AudioManager.Instance.PlaySFX("Placed Champion");
                healthBar.FadeColorBack();
            }
            else
            {
                healthBar.FadeColor();
            }
        }

        public Transform GetCharacterPosition()
        {
            return characterStand;
        }

        public Transform GetAttackerPosition()
        {
            return enemyStand;
        }

        public virtual void SetTriggerShadow()
        {
            ResetAlphaHero();

            ShadowColor.SetOnChoose();
        }

        public virtual void ResetTriggerShadow()
        {
            SetAlphaHero();

            ShadowColor.SetOriginal();
        }

        public void SetAlphaHero()
        {
            if (CanSetAlphaForHero()) sprites.SetDeadSprite();
        }

        public void ResetAlphaHero()
        {
            if (sprites != null) sprites.SetRebirthSprite();
        }

        private bool CanSetAlphaForHero()
        {
            return sprites != null && currentHero != null && currentHero != SelectionHero.SelectionHero.Instance.currentHeroAttached;
        }

        public bool AllowSwap()
        {
            if (currentHero == null) return true;
            return currentHero.IsDead == false;
        }
    }
}
