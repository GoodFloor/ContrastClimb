using Godot;

namespace ContrastClimb.characters.player;

public abstract partial class HorizontalMovement : Node
{
    protected const float MovementSpeed = 500f;
    public abstract float GetSpeed(Vector2 fromPosition);
}