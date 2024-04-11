using CombatSystem.Entity;
using LevelAndStats;
using Model.Hero;
using UnityEngine;

namespace SlotHero.SlotInGame
{
    [RequireComponent(typeof(ShadowColor))]
    public class HeroSlotInGame : MonoBehaviour
    {
        [SerializeField] private Transform characterStand;
        [SerializeField] private Transform enemyStand;
        [SerializeField] private HealthBarStatic healthBar;
        [SerializeField] private ShadowColor ShadowColor;
        
        public Transform EnemyStand => enemyStand;
        public float radius;

        public HeroCharacter currentHero;
        public int SlotIndex;
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
                var entityStats = currentHero.GetRef<EntityStats>();
                var entityAction = currentHero.GetRef<EntityAction>();
                entityAction.OnHealthChange = healthBar.SetHealthBar;
                entityStats.ChangeHealthEvent();
               // Debug.Log("Increase Scale");
                currentHero.transform.position = characterStand.position;
                AudioManager.Instance.PlaySFX("Placed Champion");
                healthBar.FadeColorBack();
            }
            else
            {
                healthBar.FadeColor();
            }
        }

        public Transform GetCharacterPosition() => characterStand;

        public Transform GetAttackerPosition() => enemyStand;
        bool triggerVFX;
        public void TriggerVFX()
        {
            if (ShadowColor.ParticleSystem.isPlaying) return;
            ShadowColor.ParticleSystem.Play();
        }
        public void StopVFX()
        {
            if (ShadowColor.ParticleSystem.isPlaying)
                ShadowColor.ParticleSystem.Stop();
        }

        public bool AllowSwap()
        {
            if (currentHero == null) return true;
            return currentHero.IsDead == false;
        }
    }
}
