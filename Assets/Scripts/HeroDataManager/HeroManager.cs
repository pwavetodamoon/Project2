using System.Collections.Generic;
using CombatSystem.HeroDataManager.Data;
using UnityEngine;

namespace CombatSystem.HeroDataManager
{
    [CreateAssetMenu(fileName = "Hero Manager", menuName = "HeroManager")]
    public class HeroManager : ScriptableObject
    {
        public List<HeroData> heroData;
        public GameObject prefabHero;

        // [Button]
        // public void SpawnHero()
        // {
        //     foreach (var data in heroData)
        //     {
        //         if (data.SlotIndex == -1) continue;
        //         var go = Instantiate(prefabHero);
        //         data.GetDataForHero(go);
        //     }
        // }

    }
}