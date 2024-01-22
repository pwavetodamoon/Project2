using UnityEngine;
using NewCombat.Characters;

namespace NewCombat.HeroAttack
{
    public class CombatCollider
    {
        private EntityCharacter entityCharacter;
        public CombatCollider(EntityCharacter entityCharacter)
        {
            this.entityCharacter = entityCharacter;
        }

        public void  CheckOverlapBox(string Tag, Vector2 point, Vector2 size, float angle = 0)
        {
            var results =  Physics2D.OverlapBoxAll(point, size, angle);
            if (results.Length == 0) return;
            foreach (var _collider in results)
            {
                if(_collider.CompareTag(Tag) == false) continue;
                if(_collider.TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage(entityCharacter.BaseStats.Strength);
                }

            }
        }
    }
}