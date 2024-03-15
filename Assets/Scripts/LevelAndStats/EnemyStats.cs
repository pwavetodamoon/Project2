namespace LevelAndStats
{
    public class EnemyStats : EntityStats
    {
        public EnemyData EnemyData;

        public void SetStructStats(StructStats stats)
        {
            structStats = stats;
        }
    }
}