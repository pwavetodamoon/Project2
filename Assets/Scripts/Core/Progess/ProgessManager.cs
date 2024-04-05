
using Core.Currency;
using Helper;
using PlayFab.ClientModels;
using UnityEngine;

namespace Core.Quest
{
    public class ProgessManager : Singleton<ProgessManager>
    {
        [SerializeField] public float point;
        [SerializeField] private StageInformation stageInformation;
        private void OnValidate()
        {
            if (stageInformation == null)
            {
                stageInformation = GetDataSupport.Get().StageInformation;
            }
        }
        public void UpdatePoint()
        {
            point = stageInformation.pointCollected;
        }

    }


}
