using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace DropItem
{
    public class RewardSignal : MonoBehaviour
    {
        [Button]
        public void SendSignal()
        {
            QuestManager.Instance.IncreasePointMonsterDead();
            RewardManager.Instance.CreateReward(transform.position);
        }
    }
}
