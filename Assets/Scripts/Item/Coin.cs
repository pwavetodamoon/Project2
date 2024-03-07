using Currency;
using UnityEngine;
using WorldText;

namespace DropItem
{
    public class Coin : BaseDrop
    {
        // Send Data to CurrencyManager
        public override void SendData()
        {
            WorldTextPool.Instance.GetText(transform.position, "+1", Color.yellow);
            CurrencyManager.Instance.AddCurrency(1);
        }
    }
}