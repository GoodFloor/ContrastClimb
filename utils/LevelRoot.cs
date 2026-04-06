using Godot;
using System;

public partial class LevelRoot : Node2D
{
    public override void _Input(InputEvent @event)
    {
        base._Input(@event);

        if (@event.IsActionPressed("pause"))
        {
            Global.GameManager.PauseGame();
        }
    }

    public override void _Notification(int what)
    {
        if (what == Window.NotificationWMGoBackRequest)
        {
            Global.GameManager.PauseGame();
        }
        else
        {
            base._Notification(what);
        }
    }
}
