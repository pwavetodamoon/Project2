using Currency;

namespace DropItem
{
    public class Coin : Items
    {
        // Send Data to CurrencyManager
        public override void SendData()
        {
            WorldTextPool.WorldTextPool.Instance.GetCoinTxt(transform.position, "+1");
            CurrencyManager.Instance.AddCurrency(1);
        }

    }
}
