using CombatSystem.HeroDataManager.Data;
using LevelAndStats;

[System.Serializable]
public class HeroCloudSaveData
{
    public string heroName;
    public int slotIndex;
    public StructStats structStats;
    public bool isDead;

    public void LoadFromHeroData(HeroData heroData)
    {
        heroData.LoadFromHeroInGame();
        heroName = heroData.heroName;
        slotIndex = heroData.slotIndex;
        structStats = heroData.structStats;
        isDead = heroData.isDead;
    }
}