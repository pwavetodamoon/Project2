using Currency;
using UnityEngine;

namespace DropItem
{
    public class Coin : Items
    {
        // Send Data to CurrencyManager
        public override void SendData()
        {
            WorldText.WorldTextPool.Instance.GetText(transform.position, "+1",Color.yellow);
            CurrencyManager.Instance.AddCurrency(1);
        }

    }
}
