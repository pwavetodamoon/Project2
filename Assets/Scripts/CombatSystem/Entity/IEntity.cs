namespace CombatSystem.Entity
{
    public interface IEntity
    {
        void RegisterObject();

        void ReleaseObject();

        bool EntityInAttackState();
    }
}