using Helper;
using UnityEngine;

namespace Core.Currency
{
    public class CurrencyManager : Singleton<CurrencyManager>
    {
        [SerializeField] public int currency;
        [SerializeField] private StageInformation stageInformation;
        [SerializeField] private MoneyConfig moneyConfig;
        private void OnValidate()
        {
            if (stageInformation == null)
            {
                stageInformation = GetDataSupport.Get().StageInformation;
            }
        }
        public void AddCurrency()
        {
            currency += moneyConfig.Get(stageInformation.GetLevelForMonster());
        }

        public void RemoveCurrency(int amount)
        {
            currency -= amount;
        }
    }
}