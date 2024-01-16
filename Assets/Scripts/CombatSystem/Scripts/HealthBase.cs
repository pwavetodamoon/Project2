using CombatSystem.Data;
using NewCombat.Characters;
using UnityEngine;

namespace CombatSystem.Scripts
{
    public class HealthBase : MonoBehaviour
    {
        [SerializeField] protected float maxHealth;
        [SerializeField] protected float minHealth = 0;
        [SerializeField] protected float currentHealth;

        // Start is called before the first frame update
        public void Setup(BaseData enemy)
        {
            minHealth = 0;
            maxHealth = enemy.health;
            currentHealth = maxHealth;
        }

        public float GetHealth()
        {
            return currentHealth;
        }

        public void ChangeHealth(float Damage)
        {
            currentHealth -= Damage;
            if (currentHealth < 0)
            {
                currentHealth = minHealth;
                if (TryGetComponent(out HeroCharacter _base))
                {
                    // CombatManager.RemoveCharacter?.Invoke(_base);
                }
                Debug.Log("EnemyCharacters is dead");
            }
        }
    }
}