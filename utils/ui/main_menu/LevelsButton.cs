using Godot;
using System;

public partial class LevelsButton : Button
{
    public override void _Pressed()
    {
        base._Pressed();
        
        Global.GameManager.OpenLevelSelection();
    }
}
