using System.Collections;

public interface ICommand
{
    bool IsDone { get; set; }
    float Time { get; set; }
    IEnumerator Execute();
}
