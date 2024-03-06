using Characters;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace NewCombat.Characters
{
    [RequireComponent(typeof(ShadowColor))]
    public class HeroSlotInGame : MonoBehaviour
    {
        [SerializeField] private Transform characterStand;
        public Transform enemyStand;
        public HeroCharacter currentHero;
        public float radius = 0;
        public int SlotIndex;

        private ShadowColor ShadowColor;

        private Character_Body_Sprites sprites;
        private void Awake()
        {
            ShadowColor = GetComponent<ShadowColor>();
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
                currentHero.transform.position = characterStand.position;
                sprites = currentHero.GetComponentInChildren<Character_Body_Sprites>();
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

        public void SetTriggerShadow()
        {
            ShadowColor.SetOnChoose();
        }

        public void ResetTriggerShadow()
        {
            ShadowColor.SetOriginal();
        }

        public bool AllowSwap()
        {
            if (currentHero == null) return true;
            return currentHero.IsDead == false;
        }
    }
}