using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace NewCombat.Characters
{
    [RequireComponent(typeof(ShadowColor))]
    public class HeroSlotInGame : MonoBehaviour
    {
        [SerializeField] private Transform characterStand;
        [SerializeField] public Transform enemyStand;
        [SerializeField] public Transform currentHero;
        [SerializeField] public float radius = 0;
        public int SlotIndex;
        private ShadowColor ShadowColor;

        private void Awake()
        {
            ShadowColor = GetComponent<ShadowColor>();
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(characterStand.position, radius);
        }

        public void SetHeroIntoStandPosition(Transform hero)
        {
            currentHero = hero;
            if (currentHero != null)
            {
                currentHero.position = characterStand.position;
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
            ShadowColor.SetOriginal();
        }

        public void DesetTriggerShadow()
        {
            ShadowColor.SetOnChoose();
        }
    }
}