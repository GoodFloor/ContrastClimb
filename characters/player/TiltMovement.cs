using Godot;

namespace ContrastClimb.characters.player;

public partial class TiltMovement : HorizontalMovement
{
    public override float GetSpeed(Vector2 fromPosition)
    {
        return HorizontalMovement.MovementSpeed * Input.GetGravity().X / 2f;
    }
}