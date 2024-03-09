using Core.Quest;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Reward
{
    public class RewardSignal : MonoBehaviour
    {
        [Button]
        public void SendSignal()
        {
            QuestManager.Instance.IncreasePointWhenKillMonster();
            RewardManager.Instance.CreateReward(transform.position);
        }
    }
}