using Godot;
using System;

public partial class ResetConfig : Button
{
    public override void _Pressed()
    {
        base._Pressed();
        Global.Config.ResetConfig();
    }
}
