using System.Collections.Generic;
using NewCombat.Characters;
using NewCombat.HeroDataManager;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using System.Linq;
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
        var list = heroManager.heroData;
        uiAvatarControllers = uiAvatarControllers.OrderBy(x => x.index).ToArray();
        for (int i = 0; i < list.Count; i++)
        {
            uiAvatarControllers[i].SetSprite(list[i].icon);
            uiAvatarControllers[i].SetHeroCharacter(list[i].heroCharacter);
        }
    }
}
