using NewCombat.Characters;

namespace NewCombat.DamageStrategy
{
    public interface IDamageStrategy
    {
        void DealDamage(IDamageable[] damageAbleArray, float damage);
    }
}