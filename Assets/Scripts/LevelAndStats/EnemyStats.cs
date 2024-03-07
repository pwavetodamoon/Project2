namespace LevelAndStats
{
    public class EnemyStats : EntityStats
    {
        public EnemyData EnemyData;

        public void LoadData()
        {
            structStats = EnemyData.structStats;
        }
    }
}