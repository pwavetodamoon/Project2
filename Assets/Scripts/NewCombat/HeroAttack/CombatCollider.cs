using NewCombat.Characters;
using System.Linq;
using UnityEngine;

namespace NewCombat.HeroAttack
{
    public static class CombatCollider
    {
        public static IDamageable[] HitBox(string Tag, Vector2 center, float radius)
        {
            var results = Physics2D.OverlapCircleAll(center, radius);

            return HitBox(results, center, Tag);
        }

        public static IDamageable[] HitBox(string Tag, Vector2 center, Vector2 size, float angle = 0)
        {
            var results = Physics2D.OverlapBoxAll(center, size, angle);

            return HitBox(results, center, Tag);
        }

        private static IDamageable[] HitBox(Collider2D[] results, Vector2 center, string Tag)
        {
            var orderedByProximity = results.OrderBy(c
                => (center - (Vector2)c.transform.position).sqrMagnitude).ToArray();
            return GetDamageAbleArray(results, Tag);
            //return null;
            //damageStrategy.DealDamage(GetDamageAbleArray(orderedByProximity, Tag), EntityStats.ActualDamage);
        }

        private static IDamageable[] GetDamageAbleArray(Collider2D[] results, string Tag)
        {
            return results.Where(x => x.CompareTag(Tag)).Select(x => x.transform.GetComponent<IDamageable>()).ToArray();
        }
    }
}