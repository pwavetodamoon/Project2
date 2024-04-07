using System;

public interface IEventListener
{
    void RegisterActionEvent(Action actionHandler);
    void UnregisterActionEvent(Action actionHandler);
}