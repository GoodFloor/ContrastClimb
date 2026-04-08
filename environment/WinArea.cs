using Godot;
using System;

public partial class WinArea : Area2D
{
    private void OnBodyEntered(Node2D body)
    {
        if (body is Player)
        {
            Global.GameManager.EndLevel(true);
        }
    }
}
