using Core.Currency;
using UnityEngine;

namespace Item
{
    public class Coin : BaseDrop
    {
        // Send Data to CurrencyManager
        public override void SendData()
        {
            WorldTextPool.WorldTextPool.Instance.GetText(transform.position, "+1", Color.yellow);
            CurrencyManager.Instance.AddCurrency(1);
        }
    }
}