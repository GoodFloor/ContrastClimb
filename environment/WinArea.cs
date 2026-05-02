using Godot;
using ContrastClimb.utils;
using ContrastClimb.characters.player;

namespace ContrastClimb.environment;

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
