using System.Collections;

namespace CombatSystem.ActionCommand
{
    public interface ICommand
    {
        float timer { get; set; }
        float duration { get; set; }
        void Execute();
    }
}
