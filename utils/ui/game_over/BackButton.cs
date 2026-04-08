using Godot;
using System;

public partial class BackButton : Button
{
    public override void _Pressed()
    {
        base._Pressed();
        
        Global.GameManager.InstantiateLoadedLevel();
        Global.GameManager.PauseGame();
    }
}
