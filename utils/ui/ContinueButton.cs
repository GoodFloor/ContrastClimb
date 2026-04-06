using Godot;
using System;

public partial class ContinueButton : Button
{
    public override void _Pressed()
    {
        base._Pressed();
        GetTree().Paused = false;
    }
}
