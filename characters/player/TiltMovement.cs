using Godot;

namespace ContrastClimb.characters.player;

public partial class TiltMovement(Player player) : Movement(player)
{
    protected override float GetVelocityX()
    {
        return Input.GetGravity().X / 2f;
    }
}