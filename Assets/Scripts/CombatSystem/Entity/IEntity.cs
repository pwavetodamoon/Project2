namespace CombatSystem.Entity
{
    public interface IEntity
    {
        void RegisterObject();

        void ReleaseObject();

        void PlayHurtAnimation();

        bool EntityInAttackState();
    }
}