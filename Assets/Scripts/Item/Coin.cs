using Core.Currency;
using Core.Quest;
using deVoid.Utils;
using UnityEngine;

namespace Item
{
    public class Coin : BaseDrop
    {
        // Send Data to CurrencyManager
        public override void SendData()
        {
            // WorldTextPool.WorldTextPool.Instance.GetText(transform.position, "+1", Color.yellow);
            CurrencyManager.Instance.AddCurrency();
            Signals.Get<SendCurrency>().Dispatch(CurrencyManager.Instance.currency);
            // Cho cap nhat ke
            ProgessManager.Instance.UpdatePoint();
            Signals.Get<SendProgessValue>().Dispatch(ProgessManager.Instance.point);
        }
    }
}