using System.Collections;

namespace CombatSystem.ActionCommand
{
    public interface ICommand
    {
        bool IsDone { get; set; }
        float Time { get; set; }
        IEnumerator Execute();
    }
}
