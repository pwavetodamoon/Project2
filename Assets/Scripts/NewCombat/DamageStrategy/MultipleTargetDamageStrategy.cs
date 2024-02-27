using NewCombat.Characters;

namespace NewCombat.DamageStrategy
{
    public class MultipleTargetDamageStrategy : IDamageStrategy
    {
        public void DealDamage(IDamageable[] damageAbleArray, float damage)
        {
            //foreach (var _damageable in damageAbleArray) _damageable.TakeDamage(damage);
        }
    }
}