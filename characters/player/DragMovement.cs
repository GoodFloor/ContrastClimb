using Godot;

namespace ContrastClimb.characters.player;

public partial class DragMovement : HorizontalMovement
{
    public override float GetSpeed(Vector2 fromPosition)
    {
        var direction = GetViewport().GetMousePosition() - fromPosition;
        direction  = direction.Normalized();
        return direction.X * MovementSpeed;
    }
}