namespace NewCombat.Characters
{
    public interface IEntity
    {
        void RegisterObject();

        void ReleaseObject();

        void PlayHurtAnimation();

        bool EntityAreNotInAttackState();
    }
}