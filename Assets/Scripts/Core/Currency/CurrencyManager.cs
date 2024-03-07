using Helper;
using UnityEngine;

namespace Core.Currency
{
    public class CurrencyManager : Singleton<CurrencyManager>
    {
        [SerializeField] public int currency;

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