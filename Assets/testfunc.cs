using System.Collections;
using System.Collections.Generic;
using NewCombat.Characters;
using NewCombat.HeroDataManager;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using System.Linq;
using Background;
using NewCombat.Slots;
using UnityEngine;
using Sirenix.Utilities;
using Characters;
using CombatSystem;
using CombatSystem.Scripts.Spawner;
using DropItem;

public class testfunc : MonoBehaviour
{
    public HeroManager heroManager;
    public UIAvatarController[] uiAvatarControllers;
    public MonsterSpawner monsterSpawner;
    public ScreenTransition screenTransition;
    public MapBackground mapBackground;

    private void Start()
    {
        Test();
        Spawn();
    }

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
        uiAvatarControllers = uiAvatarControllers.OrderBy(x => x.index).ToArray();
        for (int i = 0; i < list.Count; i++)
        {
            // ref data thứ i từ list hero data
            var heroData = list[i];
            if(heroData.heroCharacter != null) continue;

            // tạo hero từ prefab hero trong hero manager
            var hero = Instantiate(heroManager.prefabHero, CombatEntitiesManager.Instance.transform).GetComponent<HeroCharacter>();
            hero.SetAttackFactory(heroData.GetHeroFactory());

            // chỉnh sprite cho hero
            uiAvatarControllers[i].SetSprite(heroData.icon);

            // Chỉnh data cho class stats của hero
            hero.SetHeroData(heroData);

            // Chỉnh slot index cho hero bởi hero vừa tạo ra chỉ có xác 
            hero.SetSlotIndex(heroData.SlotIndex);

            // Ref hero object vào trong hero data
            heroData.heroCharacter = hero;

            // Chỉnh icon cho avatar
            uiAvatarControllers[i].SetHeroCharacter(hero);

            var heroSkin = hero.GetComponentInChildren<Character_Body_Sprites>();
            heroSkin.SetHeroSprite(heroData.GetSkinDictionary());

            SlotManager.Instance.LoadHeroIntoSlot(hero);

        }
        // Load tất cả hero vào vị trí đúng trong game
    }

    [Button]
    public void SaveSlotIndex()
    {
        var list = heroManager.heroData;
        foreach (var heroData in list)
        {
            heroData.OnSaveSlotIndex();
        }
    }

    [Button]
    public void GoNextMapSetup()
    {
        StartCoroutine(GoNextMap());
    }

    IEnumerator GoNextMap()
    {
        monsterSpawner.SetMaxSpawnCount(0);
        var list = heroManager.heroData;
        foreach (var heroData in list)
        {
            heroData.heroCharacter.SetAttackState(false);
            SlotManager.Instance.LoadHeroIntoSlot(heroData.heroCharacter);
        }

        foreach (var monster in CombatEntitiesManager.Instance.transform.GetComponentsInChildren<MonsterCharacter>())
        {
            monster.ReleaseObject();
        }
        foreach (var data in list)
        {
            yield return new WaitWhile(() => data.heroCharacter.EntityInAttackState());
            Debug.Log("Hero " + data.heroCharacter.name + " is not in attack state");
        }
        RewardManager.Instance.CollectAllItemInGame();

        yield return screenTransition.TransitionCoroutine();
        mapBackground.GoNextMap();
    }
}
