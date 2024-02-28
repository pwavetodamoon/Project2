using NewCombat.HeroDataManager;
using Sirenix.OdinInspector;
using UnityEngine;

public class testfunc : MonoBehaviour
{
    public HeroManager heroManager;

    [Button]
    public void Spawn()
    {
        heroManager.SpawnHero();
    }
}
