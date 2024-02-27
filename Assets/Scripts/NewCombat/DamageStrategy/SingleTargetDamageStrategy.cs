using NewCombat.Characters;

namespace NewCombat.DamageStrategy
{
    public class SingleTargetDamageStrategy : IDamageStrategy
    {
        public void DealDamage(IDamageable[] damageAbleArray, float damage)
        {
            foreach (var damageable in damageAbleArray)
            {
                //damageable.TakeDamage(damage);
                break;
            }
        }
    }
}