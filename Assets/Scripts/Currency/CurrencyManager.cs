using Helper;
using UnityEngine;

namespace Currency
{
    public class CurrencyManager : Singleton<CurrencyManager>
    {
        [SerializeField] private int currency = 0;

        public void AddCurrency(int amount)
        {
            currency += amount;
        }

        public void RemoveCurrency(int amount)
        {
            currency -= amount;
        }
    }
}
