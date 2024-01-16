using System.Collections;

namespace CombatSystem.ActionCommand
{
    public interface ICommandBehavior
    {
        IEnumerator FirstBehaviour();
    }
}
