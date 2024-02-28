using System.Collections.Generic;
using NewCombat.Characters;
using NewCombat.HeroDataManager;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using System.Linq;
using NewCombat.Slots;
using UnityEngine;
using Sirenix.Utilities;

public class testfunc : MonoBehaviour
{
    public HeroManager heroManager;
    public UIAvatarController[] uiAvatarControllers;
    public HeroCharacter[] heroCharacters;

    [Button]
    void Test()
    {
        uiAvatarControllers = FindObjectsOfType<UIAvatarController>();
        heroCharacters = FindObjectsOfType<HeroCharacter>();
    }

    [Button]
    public void Spawn()
    {
        // Lấy data list hero data trong hero manager
        var list = heroManager.heroData;
        var heroInGameList = new List<HeroCharacter>();
        uiAvatarControllers = uiAvatarControllers.OrderBy(x => x.index).ToArray();
        for (int i = 0; i < list.Count; i++)
        {
            // ref data thứ i từ list hero data
            var heroData = list[i];
            // tạo hero từ prefab hero trong hero manager
            var hero = Instantiate(heroManager.prefabHero, transform).GetComponent<HeroCharacter>();
            // chỉnh sprite cho hero
            uiAvatarControllers[i].SetSprite(heroData.icon);
            // Chỉnh data cho class stats của hero
            hero.SetHeroData(heroData);
            // Chỉnh slot index cho hero bởi hero vừa tạo ra chỉ có xác
            hero.InGameSlotIndex = heroData.SlotIndex;
            // Ref hero object vào trong hero data
            heroData.heroCharacter = hero;
            // Chỉnh icon cho avatar
            uiAvatarControllers[i].SetHeroCharacter(hero);
            heroInGameList.Add(hero);
        }
        // Load tất cả hero vào vị trí đúng trong game
        SlotManager.Instance.LoadHeroIntoSlotInGame(heroInGameList);
    }
}
