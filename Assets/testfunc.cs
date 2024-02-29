using System.Collections.Generic;
using NewCombat.Characters;
using NewCombat.HeroDataManager;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using System.Linq;
using NewCombat.Slots;
using UnityEngine;
using Sirenix.Utilities;
using Characters;

public class testfunc : MonoBehaviour
{
    public HeroManager heroManager;
    public UIAvatarController[] uiAvatarControllers;

    [Button]
    void Test()
    {
        uiAvatarControllers = FindObjectsOfType<UIAvatarController>();
    }

    [Button]
    public void Spawn()
    {
        // Cần phải chạy hàm Test trước để lấy danh sách UI avatar
        // Lấy data list hero data trong hero manager
        var list = heroManager.heroData;
        var heroInGameList = new List<HeroCharacter>();
        uiAvatarControllers = uiAvatarControllers.OrderBy(x => x.index).ToArray();
        for (int i = 0; i < list.Count; i++)
        {
            // ref data thứ i từ list hero data
            var heroData = list[i];
            if(heroData.heroCharacter != null) continue;
            // tạo hero từ prefab hero trong hero manager
            var hero = Instantiate(heroManager.prefabHero, transform).GetComponent<HeroCharacter>();
            hero.SetAttackFactory(heroData.GetHeroFactory());
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

            var heroSkin = hero.GetComponentInChildren<Character_Body_Sprites>();
            heroSkin.SetHeroSprite(heroData.GetSkinDictionary());
        }
        // Load tất cả hero vào vị trí đúng trong game
        SlotManager.Instance.LoadHeroIntoSlotInGame(heroInGameList);
    }
}
