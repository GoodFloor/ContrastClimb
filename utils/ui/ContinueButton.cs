using Godot;
using System;

public partial class ContinueButton : Button
{
    public override void _Pressed()
    {
        base._Pressed();
        
        Global.GameManager.ResumeGame();
    }
}
